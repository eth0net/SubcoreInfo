using HarmonyLib;
using RimWorld;
using SubcoreInfo.Comps;
using Verse;

namespace SubcoreInfo.Harmony
{
    /// <summary>
    /// Harmony_Building_SubcoreScanner_EjectContents patches subcore scanners to use our component during ejects.
    /// </summary>
    [HarmonyPatch(typeof(Building_SubcoreScanner), nameof(Building_SubcoreScanner.EjectContents))]
    internal static class Harmony_Building_SubcoreScanner_EjectContents
    {
        /// <summary>
        /// Postfix patches subcore scanners to update our component after ejecting their contents.
        /// </summary>
        /// <param name="__instance"></param>
        internal static void Postfix(Building_SubcoreScanner __instance)
        {
            __instance.GetComp<CompEjected>().Ejected = true;
        }
    }

    /// <summary>
    /// Harmony_Building_SubcoreScanner_Tick patches subcore scanners to use our component during ticks.
    /// </summary>
    [HarmonyPatch(typeof(Building_SubcoreScanner), nameof(Building_SubcoreScanner.Tick))]
    internal static class Harmony_Building_SubcoreScanner_Tick
    {
        /// <summary>
        /// Prefix stores the name of the current occupant for later use.
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__state"></param>
        internal static void Prefix(Building_SubcoreScanner __instance, ref Name __state)
        {
            __state = __instance?.Occupant?.Name ?? null;
        }

        /// <summary>
        /// Postfix attempts to update the subcore if one was just ejected by the scanner.
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__state"></param>
        internal static void Postfix(Building_SubcoreScanner __instance, Name __state)
        {
            CompSubcoreInfo comp = TryGetSubcoreComp(__instance);
            if (comp == null) return;

            comp.PatternName = __state;
        }

        /// <summary>
        /// Try to find kekethe subcore ejected from the scanner and return the component for it.
        /// </summary>
        /// <param name="scanner"></param>
        /// <returns></returns>
        static CompSubcoreInfo TryGetSubcoreComp(Building_SubcoreScanner scanner)
        {
            ThingDef subcoreDef = scanner.def.defName switch
            {
                "SubcoreSoftscanner" => ThingDef.Named("SubcoreRegular"),
                "SubcoreRipscanner" => ThingDef.Named("SubcoreHigh"),
                _ => null
            };

            if (subcoreDef == null) { return null; }

            static bool validator(Thing subcore)
            {
                CompSubcoreInfo comp = subcore.TryGetComp<CompSubcoreInfo>();
                return comp != null && comp.PatternName == null;
            }

            Thing subcore = GenClosest.ClosestThingReachable(scanner.InteractionCell, scanner.Map, ThingRequest.ForDef(subcoreDef), Verse.AI.PathEndMode.ClosestTouch, TraverseParms.For(TraverseMode.ByPawn), 9999, validator);

            return subcore?.TryGetComp<CompSubcoreInfo>() ?? null;
        }
    }
}
