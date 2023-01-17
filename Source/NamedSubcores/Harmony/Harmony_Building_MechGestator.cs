using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;

namespace NamedSubcores
{
    internal class Harmony_Building_MechGestator
    {
        /// <summary>
        /// Harmony_Building_MechGestator_Notify_AllGestationCyclesCompleted patches mech gestators to use our component on completion.
        /// </summary>
        [HarmonyPatch(typeof(Building_MechGestator), "Notify_AllGestationCyclesCompleted")]
        internal static class Harmony_Building_MechGestator_Notify_AllGestationCyclesCompleted
        {
            /// <summary>
            /// Prefix stores the subcore pattern for later use.
            /// </summary>
            /// <param name="__instance"></param>
            internal static void Prefix(Building_MechGestator __instance)
            {
                static bool hasNamedSubcoreComp(Thing thing) => (thing?.TryGetComp<SubcorePatternComp>() ?? null) != null;
                Thing subcore = __instance.innerContainer.FirstOrDefault(hasNamedSubcoreComp);
                if (subcore == null) { return; }

                SubcorePatternComp subcoreComp = subcore.TryGetComp<SubcorePatternComp>();
                if (subcoreComp == null) { return; }

                MechGestatorComp gestatorComp = __instance.GetComp<MechGestatorComp>();
                if (gestatorComp == null) { return; }

                gestatorComp.PatternName = subcoreComp.PatternName;
            }

            /// <summary>
            /// Postfix copies data from the subcore to the new mech.
            /// </summary>
            /// <param name="__instance"></param>
            internal static void Postfix(Building_MechGestator __instance)
            {
                MechGestatorComp gestatorComp = __instance.GetComp<MechGestatorComp>();
                if (gestatorComp == null) { return; }

                MechPatternComp mechComp = __instance.GestatingMech.GetComp<MechPatternComp>();
                if (mechComp == null) { return; }

                mechComp.PatternName = gestatorComp.PatternName;
            }
        }
    }
}
