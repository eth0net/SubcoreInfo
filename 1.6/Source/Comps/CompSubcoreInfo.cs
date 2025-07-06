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
        if (!base.AllowStackWith(other))
        {
            return false;
        }

        if (!SubcoreInfoSettings.separateStacks)
        {
            return true;
        }

        CompSubcoreInfo otherComp = other?.TryGetComp<CompSubcoreInfo>();
        if (otherComp == null)
        {
            return false;
        }

        bool nameMatches = PawnName == otherComp.PawnName;
        bool titleMatches = TitleName == otherComp.TitleName;
        bool factionMatches = FactionName == otherComp.FactionName;

        return nameMatches && titleMatches && factionMatches;
    }
    
    /// <summary>
    /// PostPostGeneratedForTrader is called after the subcore is generated for a trader.
    /// </summary>
    /// <param name="trader"></param>
    /// <param name="forTile"></param>
    /// <param name="forFaction"></param>
    public override void PostPostGeneratedForTrader(TraderKindDef trader, PlanetTile forTile, Faction forFaction)
    {
        base.PostPostGeneratedForTrader(trader, forTile, forFaction);

        if (!SubcoreInfoSettings.randomTraderInfo)
        {
            return;
        }

        var pawnFaction = forFaction;
        if (forFaction == null)
        {
            var randomFactions = Find.FactionManager.AllFactions.Where(ValidFaction).ToList();
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

    /// <summary>
    /// ValidFaction checks if a faction could be used to generate subcore info for traders.
    /// </summary>
    /// <param name="faction">Faction being considered.</param>
    private static bool ValidFaction(Faction faction)
    {
        return faction != null && !faction.IsPlayer && !faction.temporary && !faction.Hidden && faction.def.humanlikeFaction;
    }
}
