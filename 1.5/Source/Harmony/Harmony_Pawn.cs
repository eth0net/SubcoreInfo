using HarmonyLib;
using SubcoreInfo.Comps;
using System.Linq;
using Verse;
using Verse.AI;

namespace SubcoreInfo.Harmony;

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
        if (__instance == null || !__instance.IsColonyMech)
        {
            return;
        }

        CompMechInfo mechComp = __instance.GetComp<CompMechInfo>();
        if (mechComp == null || !mechComp.Disassembling)
        {
            return;
        }

        Thing subcore = TryGetSubcore(__instance);
        if (subcore == null)
        {
            return;
        }

        SubcoreInfoUtility.CopySubcoreInfo(__instance, subcore as ThingWithComps);
    }

    /// <summary>
    /// Try to find the subcore dropped during disassembly and return it.
    /// </summary>
    /// <param name="scanner"></param>
    /// <returns></returns>
    static Thing TryGetSubcore(Pawn mech)
    {
        ThingDefCountClass subcoreClass = MechanitorUtility.IngredientsFromDisassembly(mech.def).FirstOrDefault((ThingDefCountClass thing) => thing.thingDef.defName == "SubcoreRegular" || thing.thingDef.defName == "SubcoreHigh");
        if (subcoreClass == null)
        {
            return null;
        }

        static bool validator(Thing subcore)
        {
            CompSubcoreInfo comp = subcore.TryGetComp<CompSubcoreInfo>();
            if (comp == null)
            {
                return false;
            }
            return comp.PawnName == null && comp.TitleName == null && comp.FactionName == null;
        }

        return GenClosest.ClosestThingReachable(mech.Position, mech.Map, ThingRequest.ForDef(subcoreClass.thingDef), PathEndMode.ClosestTouch, TraverseParms.For(TraverseMode.ByPawn), 9999, validator);
    }
}
