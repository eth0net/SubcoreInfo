﻿using HarmonyLib;
using RimWorld;
using Verse;

namespace SubcoreInfo
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
            __instance.GetComp<SubcoreScannerPatternComp>().Ejected = true;
        }
    }

    /// <summary>
    /// Harmony_Building_SubcoreScanner_Tick patches subcore scanners to use our component during ticks.
    /// </summary>
    [HarmonyPatch(typeof(Building_SubcoreScanner), "Tick")]
    internal static class Harmony_Building_SubcoreScanner_Tick
    {
        /// <summary>
        /// Prefix stores the name of the current occupant for later use.
        /// </summary>
        /// <param name="__instance"></param>
        internal static void Prefix(Building_SubcoreScanner __instance)
        {
            SubcoreScannerPatternComp scannerComp = __instance.GetComp<SubcoreScannerPatternComp>();
            scannerComp.PatternName = __instance?.Occupant?.Name ?? null;
        }

        /// <summary>
        /// Postfix attempts to update the subcore if one was just ejected by the scanner.
        /// </summary>
        /// <param name="__instance"></param>
        internal static void Postfix(Building_SubcoreScanner __instance)
        {
            SubcoreScannerPatternComp scannerComp = __instance.GetComp<SubcoreScannerPatternComp>();
            if (!scannerComp.Ejected) { return; }

            SubcoreInfoComp subcoreComp = TryGetSubcoreComp(__instance);
            if (subcoreComp != null)
            {
                subcoreComp.PatternName = scannerComp.PatternName;
            }

            scannerComp.Reset();
        }

        /// <summary>
        /// Try to find kekethe subcore ejected from the scanner and return the component for it.
        /// </summary>
        /// <param name="scanner"></param>
        /// <returns></returns>
        static SubcoreInfoComp TryGetSubcoreComp(Building_SubcoreScanner scanner)
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
                SubcoreInfoComp comp = subcore.TryGetComp<SubcoreInfoComp>();
                if (comp == null) { return false; }
                return comp.PatternName == null;
            }

            Thing subcore = GenClosest.ClosestThingReachable(scanner.InteractionCell, scanner.Map, ThingRequest.ForDef(subcoreDef), Verse.AI.PathEndMode.ClosestTouch, TraverseParms.For(TraverseMode.ByPawn), 9999, validator);

            return subcore?.TryGetComp<SubcoreInfoComp>() ?? null;
        }
    }
}