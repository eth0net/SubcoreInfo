using Verse;

namespace SubcoreInfo.Comps
{
    public class CompPatternBase : ThingComp
    {
        /// <summary>
        /// PatternName tracks the name of the pawn scanned.
        /// </summary>
        public Name PatternName;

        /// <summary>
        /// PostExposeData is used to save our component state.
        /// </summary>
        public override void PostExposeData()
        {
            Scribe_Deep.Look(ref PatternName, "patternName");
            base.PostExposeData();
        }
    }
}