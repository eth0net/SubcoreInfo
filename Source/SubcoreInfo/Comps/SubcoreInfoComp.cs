using RimWorld;
using Verse;

namespace SubcoreInfo
{
    /// <summary>
    /// SubcoreInfoComp is added to subcores and is used to track the pawn scanned into the subcore.
    /// </summary>
    public class SubcoreInfoComp : BaseInfoComp
    {
        /// <summary>
        /// AllowStackWith ensures that subcores can only be stacked with others of the same pattern.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool AllowStackWith(Thing other)
        {
            if (base.AllowStackWith(other) == false) { return false; };

            SubcoreInfoComp otherComp = other?.TryGetComp<SubcoreInfoComp>();
            if (otherComp == null) { return false; }

            return PatternName == otherComp.PatternName;
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

            Gender gender = Rand.Bool ? Gender.Male : Gender.Female;
            PawnKindDef pawnKind = forFaction.RandomPawnKind();
            RulePackDef pawnKindNameMaker = pawnKind.GetNameMaker(gender);
            RulePackDef raceNameGenerator = pawnKind.RaceProps.GetNameGenerator(gender);

            PatternName = PawnBioAndNameGenerator.GenerateFullPawnName(parent.def, pawnKindNameMaker, null, null, raceNameGenerator, null, gender);
        }
    }
}
