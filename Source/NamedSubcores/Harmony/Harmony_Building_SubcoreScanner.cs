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
            scannerComp.OccupantName = __instance?.Occupant?.Name ?? null;
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
                    subcoreComp.OccupantName = scannerComp.OccupantName;
                    scannerComp.OccupantName = null;
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
            ThingDef subcoreDef = scanner.def.defName switch
            {
                "SubcoreSoftscanner" => ThingDef.Named("SubcoreRegular"),
                "SubcoreRipscanner" => ThingDef.Named("SubcoreHigh"),
                _ => null
            };

            if (subcoreDef != null)
            {
                static bool validator(Thing subcore)
                {
                    NamedSubcoreComp comp = subcore.TryGetComp<NamedSubcoreComp>();
                    return comp != null && comp.OccupantName == null;
                }

                Thing subcore = GenClosest.ClosestThingReachable(scanner.InteractionCell, scanner.Map, ThingRequest.ForDef(subcoreDef), Verse.AI.PathEndMode.ClosestTouch, TraverseParms.For(TraverseMode.ByPawn), 9999, validator);

                return subcore?.TryGetComp<NamedSubcoreComp>() ?? null;
            }

            return null;
        }
    }
}
