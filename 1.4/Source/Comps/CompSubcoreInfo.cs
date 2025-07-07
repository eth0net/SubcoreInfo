using System.Collections.Generic;
using RimWorld;
using System.Linq;
using RimWorld.Planet;
using Verse;

namespace SubcoreInfo.Comps;

/// <summary>
/// CompSubcoreInfo is added to subcores and is used to track the pawn scanned into the subcore.
/// </summary>
public class CompSubcoreInfo : CompDisplayInfo
{
    /// <summary>
    /// AllowStackWith ensures that subcores can only be stacked with others of the same pattern.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public override bool AllowStackWith(Thing other)
    {
        if (!base.AllowStackWith(other)) return false;

        if (!SubcoreInfoSettings.separateStacks) return true;

        if (other?.TryGetComp<CompSubcoreInfo>() is not { } otherComp)
        {
            return false;
        }

        if (Pawn != otherComp.Pawn) return false;
        if (PawnName != otherComp.PawnName) return false;
        if (TitleName != otherComp.TitleName) return false;
        if (FactionName != otherComp.FactionName) return false;
        if (IdeoName != otherComp.IdeoName) return false;

        return true;
    }

    /// <summary>
    /// PostPostGeneratedForTrader is called after the subcore is generated for a trader.
    /// </summary>
    /// <param name="trader"></param>
    /// <param name="forTile"></param>
    /// <param name="forFaction"></param>
    public override void PostPostGeneratedForTrader(TraderKindDef trader, int forTile, Faction forFaction)
    {
        base.PostPostGeneratedForTrader(trader, forTile, forFaction);

        if (!SubcoreInfoSettings.randomTraderInfo) return;

        Faction pawnFaction = forFaction ?? RandomValidFaction();
        if (pawnFaction is null)
        {
            Log.Warning("SubcoreInfo: No valid faction found for generating subcore info for trader.");
            return;
        }

        Copy(PawnGenerator.GeneratePawn(pawnFaction.RandomPawnKind(), pawnFaction));
    }

    private static Faction RandomValidFaction()
    {
        IEnumerable<Faction> randomFactions = Find.FactionManager.AllFactions.Where(IsValidFaction);
        return randomFactions.RandomElement();
    }

    /// <summary>
    /// IsValidFaction checks if a faction could be used to generate subcore info for traders.
    /// </summary>
    /// <param name="faction">Faction being considered.</param>
    private static bool IsValidFaction(Faction faction)
    {
        return faction is { IsPlayer: false, temporary: false, Hidden: false } && faction.def.humanlikeFaction;
    }
}
