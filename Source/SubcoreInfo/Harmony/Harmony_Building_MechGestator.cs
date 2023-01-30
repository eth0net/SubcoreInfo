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
            /// <param name="__state"></param>
            internal static void Prefix(Building_MechGestator __instance, ref Name __state)
            {
                static bool hasNamedSubcoreComp(Thing thing) => (thing?.TryGetComp<CompSubcoreInfo>() ?? null) != null;
                Thing subcore = __instance.innerContainer.FirstOrDefault(hasNamedSubcoreComp);
                if (subcore == null) { return; }

                CompSubcoreInfo subcoreComp = subcore.TryGetComp<CompSubcoreInfo>();
                if (subcoreComp == null) { return; }

                __state = subcoreComp.PatternName;
            }

            /// <summary>
            /// Postfix copies data from the subcore to the new mech.
            /// </summary>
            /// <param name="__instance"></param>
            /// <param name="__state"></param>
            internal static void Postfix(Building_MechGestator __instance, Name __state)
            {
                CompMechInfo mechComp = __instance.GestatingMech.GetComp<CompMechInfo>();
                if (mechComp == null) { return; }

                mechComp.PatternName = __state;
            }
        }
    }
}
