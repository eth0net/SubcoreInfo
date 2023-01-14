using HarmonyLib;
using RimWorld;
using Verse;

namespace NamedSubcores
{
    /// <summary>
    /// Harmony_Building_SubcoreScanner_EjectContents patches subcore scanners to use our component during ejects.
    /// </summary>
    [HarmonyPatch(typeof(Building_SubcoreScanner), "EjectContents")]
    internal static class Harmony_Building_SubcoreScanner_EjectContents
    {
        /// <summary>
        /// Postfix patches subcore scanners to update our component after ejecting their contents.
        /// </summary>
        /// <param name="__instance"></param>
        internal static void Postfix(Building_SubcoreScanner __instance)
        {
            __instance.GetComp<NamedSubcoreScannerComp>().Ejected = true;
        }
    }

    /// <summary>
    /// Harmony_Building_SubcoreScanner_Tick patches subcore scanners to use our component during ticks.
    /// </summary>
    [HarmonyPatch(typeof(Building_SubcoreScanner), "Tick")]
    internal static class Harmony_Building_SubcoreScanner_Tick
    {
        /// <summary>
        /// Prefix stores the name of the current occupant for later use
        /// </summary>
        /// <param name="__instance"></param>
        internal static void Prefix(Building_SubcoreScanner __instance)
        {
            NamedSubcoreScannerComp scannerComp = __instance.GetComp<NamedSubcoreScannerComp>();
            scannerComp.PawnName = __instance?.Occupant?.Name ?? null;
        }

        /// <summary>
        /// Postfix attempts to name the subcore if one was just ejected by the scanner
        /// </summary>
        /// <param name="__instance"></param>
        internal static void Postfix(Building_SubcoreScanner __instance)
        {
            NamedSubcoreScannerComp scannerComp = __instance.GetComp<NamedSubcoreScannerComp>();
            if (scannerComp.Ejected)
            {
                NamedSubcoreComp subcoreComp = TryGetSubcoreComp(__instance);
                if (subcoreComp != null)
                {
                    subcoreComp.PawnName = scannerComp.PawnName;
                    scannerComp.PawnName = null;
                }
                scannerComp.Ejected = false;
            }
        }

        /// <summary>
        /// Try to find the subcore ejected from the scanner and return the component for it
        /// </summary>
        /// <param name="scanner"></param>
        /// <returns></returns>
        static NamedSubcoreComp TryGetSubcoreComp(Building_SubcoreScanner scanner)
        {
            string subcoreDefName = scanner.def.defName switch
            {
                "SubcoreSoftscanner" => "SubcoreRegular",
                "SubcoreRipscanner" => "SubcoreHigh",
                _ => null
            };

            Log.Warning("Scanner Def Name: "+scanner.def.defName);
            Log.Warning("Subcore Def Name: "+subcoreDefName);

            if (subcoreDefName != null)
            {
                // bad cell


                foreach (IntVec3 cell in scanner.InteractionCells)
                {
                    foreach (Thing thing in cell.GetThingList(scanner.Map))
                    {
                        if (thing.def.defName == subcoreDefName)
                        {
                            Log.Message("Found matching subcore");
                            NamedSubcoreComp comp = thing.TryGetComp<NamedSubcoreComp>();
                            Log.Message("Got subcore comp");
                            if (comp != null && comp.PawnName == null)
                            {
                                Log.Message("Returning comp: " + comp);
                                return comp;
                            }
                        }
                    }
                }
            }
            Log.Error("No comp");
            return null;
        }
    }
}
