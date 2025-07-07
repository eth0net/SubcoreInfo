using HarmonyLib;
using RimWorld;
using SubcoreInfo.Comps;
using Verse;
using Verse.AI;

namespace SubcoreInfo.Harmony;

/// <summary>
/// Harmony_JobDriver_DisassembleMech patches mech disassembling to use our component.
/// </summary>
[HarmonyPatch(typeof(JobDriver_DisassembleMech), "MakeNewToils")]
internal static class Harmony_JobDriver_DisassembleMech_MakeNewToils
{
    /// <summary>
    /// Prefix is called before the original method.
    /// </summary>
    /// <param name="__instance"></param>
    internal static void Prefix(JobDriver_DisassembleMech __instance)
    {
        Pawn mech = (Pawn) __instance.job.GetTarget(TargetIndex.A).Thing;

        CompMechInfo mechComp = mech?.GetComp<CompMechInfo>();
        if (mechComp == null) return;

        mechComp.Disassembling = true;
    }
}
