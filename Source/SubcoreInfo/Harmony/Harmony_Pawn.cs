using HarmonyLib;
using SubcoreInfo.Comps;
using System.Linq;
using Verse;
using Verse.AI;

namespace SubcoreInfo.Harmony
{
    internal class Harmony_Pawn
    {
        /// <summary>
        /// Harmony_JobDriver_DisassembleMech patches mech disassembling to use our component.
        /// </summary>
        [HarmonyPatch(typeof(Pawn), nameof(Pawn.Kill))]
        internal static class Harmony_Pawn_Kill
        {
            /// <summary>
            /// Prefix is called before the original method.
            /// </summary>
            /// <param name="__instance"></param>
            internal static void Prefix(Pawn __instance)
            {
                if (__instance == null || !__instance.IsColonyMech) { return; }

                MechInfoComp mechComp = __instance.GetComp<MechInfoComp>();
                if (mechComp == null || !mechComp.Disassembling) { return; }

                SubcoreInfoComp subcoreComp = TryGetSubcoreComp(__instance);
                if (subcoreComp == null) { return; }

                subcoreComp.PatternName = mechComp.PatternName;
                mechComp.Disassembling = false;
            }

            /// <summary>
            /// Try to find the subcore dropped during disassembly and return the component for it.
            /// </summary>
            /// <param name="scanner"></param>
            /// <returns></returns>
            static SubcoreInfoComp TryGetSubcoreComp(Pawn mech)
            {
                ThingDefCountClass subcoreClass = MechanitorUtility.IngredientsFromDisassembly(mech.def).FirstOrDefault((ThingDefCountClass thing) => thing.thingDef.defName == "SubcoreRegular" || thing.thingDef.defName == "SubcoreHigh");
                if (subcoreClass == null) { return null; }

                static bool validator(Thing subcore)
                {
                    SubcoreInfoComp comp = subcore.TryGetComp<SubcoreInfoComp>();
                    if (comp == null) { return false; }
                    return comp.PatternName == null;
                }

                Thing subcore = GenClosest.ClosestThingReachable(mech.Position, mech.Map, ThingRequest.ForDef(subcoreClass.thingDef), PathEndMode.ClosestTouch, TraverseParms.For(TraverseMode.ByPawn), 9999, validator);

                return subcore?.TryGetComp<SubcoreInfoComp>() ?? null;
            }
        }
    }
}
