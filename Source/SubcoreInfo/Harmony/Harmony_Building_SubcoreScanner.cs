using HarmonyLib;
using RimWorld;
using SubcoreInfo.Comps;
using System;
using System.Collections.Generic;
using Verse;

namespace SubcoreInfo.Harmony
{
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
        internal static void Prefix(Building_SubcoreScanner __instance)
        {
            __instance.GetComp<CompPatternInfo>().PatternName = __instance?.Occupant?.Name;
        }

        /// <summary>
        /// Transpiler replaces the call to place the subcore with a modded call that also updates the subcore pattern.
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) => instructions.MethodReplacer(
            AccessTools.Method(typeof(GenPlace), nameof(GenPlace.TryPlaceThing), new Type[] { typeof(Thing), typeof(IntVec3), typeof(Map), typeof(ThingPlaceMode), typeof(Action<Thing, int>), typeof(Predicate<IntVec3>), typeof(Rot4) }),
            AccessTools.Method(typeof(Harmony_Building_SubcoreScanner_Tick), nameof(Harmony_Building_SubcoreScanner_Tick.TryUpdateAndPlaceSubcore))
        );

        /// <summary>
        /// TryUpdateAndPlaceSubcore attempts to update the subcore info and place it in the world.
        /// </summary>
        /// <param name="thing"></param>
        /// <param name="center"></param>
        /// <param name="map"></param>
        /// <param name="mode"></param>
        /// <param name="placedAction"></param>
        /// <param name="nearPlaceValidator"></param>
        /// <param name="rot"></param>
        /// <returns></returns>
        static bool TryUpdateAndPlaceSubcore(Thing thing, IntVec3 center, Map map, ThingPlaceMode mode, Action<Thing, int> placedAction = null, Predicate<IntVec3> nearPlaceValidator = null, Rot4 rot = default(Rot4))
        {
            ThingDef scannerDef = thing.def.defName switch
            {
                "SubcoreRegular" => ThingDef.Named("SubcoreSoftscanner"),
                "SubcoreHigh" => ThingDef.Named("SubcoreRipscanner"),
                _ => null
            };

            if (scannerDef != null)
            {
                Thing scanner = GenClosest.ClosestThing_Global(center, map.listerBuldingOfDefInProximity.GetForCell(center, 5, scannerDef));
                CompPatternInfo scannerComp = scanner.TryGetComp<CompPatternInfo>();
                CompSubcoreInfo subcoreComp = thing.TryGetComp<CompSubcoreInfo>();
                subcoreComp.PatternName = scannerComp?.PatternName;
            }

            return GenPlace.TryPlaceThing(thing, center, map, mode);
        }
    }
}
