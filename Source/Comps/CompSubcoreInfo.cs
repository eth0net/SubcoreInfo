using RimWorld;
using Verse;

namespace SubcoreInfo.Comps
{
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
        /// PostPostGeneratedForTrader is called after the subcore is generated for a trader.
        /// </summary>
        /// <param name="trader"></param>
        /// <param name="forTile"></param>
        /// <param name="forFaction"></param>
        public override void PostPostGeneratedForTrader(TraderKindDef trader, int forTile, Faction forFaction)
        {
            base.PostPostGeneratedForTrader(trader, forTile, forFaction);

            if (!SubcoreInfoSettings.randomTraderInfo) { return; }

            Copy(PawnGenerator.GeneratePawn(forFaction.RandomPawnKind(), forFaction));
        }
    }
}
