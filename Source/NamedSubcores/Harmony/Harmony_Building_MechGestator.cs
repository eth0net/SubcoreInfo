using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;

namespace NamedSubcores
{
    internal class Harmony_Building_MechGestator
    {
        // Building_MechGestator.Notify_AllGestationCyclesCompleted
        // prefix: grab subcore comp from innercontainer
        // postfix: assign comp to pawn

        /// <summary>
        /// Harmony_Building_MechGestator_Notify_AllGestationCyclesCompleted patches mech gestators to use our component on completion.
        /// </summary>
        [HarmonyPatch(typeof(Building_MechGestator), "Notify_AllGestationCyclesCompleted")]
        internal static class Harmony_Building_MechGestator_Notify_AllGestationCyclesCompleted
        {
            /// <summary>
            /// Prefix stores the named subcore component for later use.
            /// </summary>
            /// <param name="__instance"></param>
            internal static void Prefix(Building_MechGestator __instance)
            {
                Func<Thing, bool> hasNamedSubcoreComp = (thing) => (thing?.TryGetComp<NamedSubcoreComp>() ?? null) != null;
                Thing subcore = __instance.innerContainer.FirstOrDefault(hasNamedSubcoreComp);
                if (subcore == null) { return; }

                NamedMechGestatorComp gestatorComp = __instance.GetComp<NamedMechGestatorComp>();
                if (gestatorComp == null) { return; }

                gestatorComp.SubcoreOccupantName = subcore.TryGetComp<NamedSubcoreComp>()?.OccupantName;
            }

            /// <summary>
            /// Postfix assigns the named subcore component to the new mech.
            /// </summary>
            /// <param name="__instance"></param>
            internal static void Postfix(Building_MechGestator __instance)
            {
                NamedMechGestatorComp gestatorComp = __instance.GetComp<NamedMechGestatorComp>();
                if (gestatorComp == null) { return; }

                NamedMechComp mechComp = __instance.GestatingMech.GetComp<NamedMechComp>();
                if (mechComp == null) { return; }

                mechComp.OccupantName = gestatorComp.SubcoreOccupantName;
            }
        }
    }
}
