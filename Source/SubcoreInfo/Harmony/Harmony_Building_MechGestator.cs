using HarmonyLib;
using RimWorld;
using SubcoreInfo.Comps;
using System.Linq;
using Verse;

namespace SubcoreInfo.Harmony
{
    internal class Harmony_Building_MechGestator
    {
        /// <summary>
        /// Harmony_Building_MechGestator_Notify_AllGestationCyclesCompleted patches mech gestators to use our component on completion.
        /// </summary>
        [HarmonyPatch(typeof(Building_MechGestator), nameof(Building_MechGestator.Notify_AllGestationCyclesCompleted))]
        internal static class Harmony_Building_MechGestator_Notify_AllGestationCyclesCompleted
        {
            /// <summary>
            /// Prefix stores the subcore pattern for later use.
            /// </summary>
            /// <param name="__instance"></param>
            internal static void Prefix(Building_MechGestator __instance)
            {
                static bool hasNamedSubcoreComp(Thing thing) => (thing?.TryGetComp<SubcoreInfoComp>() ?? null) != null;
                Thing subcore = __instance.innerContainer.FirstOrDefault(hasNamedSubcoreComp);
                if (subcore == null) { return; }

                SubcoreInfoComp subcoreComp = subcore.TryGetComp<SubcoreInfoComp>();
                if (subcoreComp == null) { return; }

                MechGestatorPatternComp gestatorComp = __instance.GetComp<MechGestatorPatternComp>();
                if (gestatorComp == null) { return; }

                gestatorComp.PatternName = subcoreComp.PatternName;
            }

            /// <summary>
            /// Postfix copies data from the subcore to the new mech.
            /// </summary>
            /// <param name="__instance"></param>
            internal static void Postfix(Building_MechGestator __instance)
            {
                MechGestatorPatternComp gestatorComp = __instance.GetComp<MechGestatorPatternComp>();
                if (gestatorComp == null) { return; }

                MechInfoComp mechComp = __instance.GestatingMech.GetComp<MechInfoComp>();
                if (mechComp == null) { return; }

                mechComp.PatternName = gestatorComp.PatternName;
            }
        }
    }
}
