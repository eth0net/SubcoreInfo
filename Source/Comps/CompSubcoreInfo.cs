using System.Linq;
using RimWorld;
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
        if (!base.AllowStackWith(other)) { return false; }

        if (!SubcoreInfoSettings.separateStacks) { return true; }

        CompSubcoreInfo otherComp = other?.TryGetComp<CompSubcoreInfo>();
        if (otherComp == null) { return false; }

        return PawnName == otherComp.PawnName && TitleName == otherComp.TitleName && FactionName == otherComp.FactionName;
    }

    /// <summary>
    /// Checks if a random faction could be used to generate subcore information for traders.
    /// </summary>
    /// <param name="faction">Faction being considered.</param>
    private static bool ValidRandomFaction(Faction faction)
    {
        return faction != null && !faction.IsPlayer && !faction.temporary && !faction.Hidden && faction.def.humanlikeFaction;
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

        if (!SubcoreInfoSettings.randomTraderInfo) { return; }

        var pawnFaction = forFaction;
        if (pawnFaction == null)
        {
            var randomFactions = Find.FactionManager.AllFactions.Where(ValidRandomFaction).ToList();
            if (!randomFactions.NullOrEmpty())
            {
                pawnFaction = randomFactions.RandomElement();
            }
        }

        if (pawnFaction != null)
        {
            Copy(PawnGenerator.GeneratePawn(pawnFaction.RandomPawnKind(), forFaction));
        }
    }
}
