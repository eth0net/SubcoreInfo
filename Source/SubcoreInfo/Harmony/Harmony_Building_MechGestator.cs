using HarmonyLib;
using RimWorld;
using SubcoreInfo.Comps;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SubcoreInfo.Harmony
{
    /// <summary>
    /// Harmony_Building_MechGestator_Notify_AllGestationCyclesCompleted patches mech gestators to use our component on completion.
    /// </summary>
    [HarmonyPatch(typeof(Building_MechGestator), nameof(Building_MechGestator.Notify_AllGestationCyclesCompleted))]
    internal static class Harmony_Building_MechGestator_Notify_AllGestationCyclesCompleted
    {
        /// <summary>
        /// Transpiler replaces the call to add the mechanoid with a modded call that also updates the mechanoid pattern.
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) => instructions.MethodReplacer(
            AccessTools.Method(typeof(ThingOwner), nameof(ThingOwner.ClearAndDestroyContents), new Type[] { typeof(DestroyMode) }),
            AccessTools.Method(typeof(Harmony_Building_MechGestator_Notify_AllGestationCyclesCompleted), nameof(UpdateThenClearAndDestroyContents))
        ).MethodReplacer(
            AccessTools.Method(typeof(ThingOwner), nameof(ThingOwner.TryAdd), new Type[] { typeof(Thing), typeof(bool) }),
            AccessTools.Method(typeof(Harmony_Building_MechGestator_Notify_AllGestationCyclesCompleted), nameof(TryUpdateAndAddPawn))
        );

        /// <summary>
        /// UpdateThenClearAndDestroyContents stores the pattern name before clearing the contents.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="mode"></param>
        static void UpdateThenClearAndDestroyContents(ThingOwner owner, DestroyMode mode = DestroyMode.Vanish)
        {
            Thing subcore = owner.FirstOrDefault(HasCompSubcoreInfo);

            if (subcore != null)
            {
                CompPatternBase gestatorComp = ((Building_MechGestator)owner.Owner).GetComp<CompPatternBase>();
                CompSubcoreInfo subcoreComp = ((ThingWithComps)subcore).GetComp<CompSubcoreInfo>();
                gestatorComp.PatternName = subcoreComp.PatternName;
            }

            owner.ClearAndDestroyContents(mode);
        }

        /// <summary>
        /// TryUpdateAndAddPawn updates the mech pattern before adding it.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="thing"></param>
        /// <param name="canMergeWithExistingStacks"></param>
        /// <returns></returns>
        static bool TryUpdateAndAddPawn(ThingOwner owner, Thing thing, bool canMergeWithExistingStacks = true)
        {
            CompPatternBase gestatorComp = ((Building_MechGestator)owner.Owner).GetComp<CompPatternBase>();
            CompMechInfo mechComp = ((ThingWithComps)thing).GetComp<CompMechInfo>();

            mechComp.PatternName = gestatorComp.PatternName;
            gestatorComp.PatternName = null;

            return owner.TryAdd(thing, canMergeWithExistingStacks);
        }

        static bool HasCompSubcoreInfo(Thing thing) => thing?.TryGetComp<CompSubcoreInfo>() != null;
    }
}
