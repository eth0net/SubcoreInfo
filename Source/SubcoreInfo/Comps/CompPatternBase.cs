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
        /// TitleName tracks the title of the pawn scanned.
        /// </summary>
        public string TitleName;

        /// <summary>
        /// FactionName tracks the name of the faction of the pawn scanned.
        /// </summary>
        public string FactionName;

        /// <summary>
        /// PostExposeData is used to save our component state.
        /// </summary>
        public override void PostExposeData()
        {
            Scribe_Deep.Look(ref PatternName, "patternName");
            Scribe_Values.Look(ref TitleName, "titleName");
            Scribe_Values.Look(ref FactionName, "factionName");
            base.PostExposeData();
        }

        /// <summary>
        /// Copy loads the data from another comp into this one to ease data transfer.
        /// </summary>
        /// <param name="src"></param>
        public void Copy(CompPatternBase src)
        {
            PatternName = src.PatternName;
            TitleName = src.TitleName;
            FactionName = src.FactionName;
        }

        /// <summary>
        /// Reset clears all data in this comp.
        /// </summary>
        public void Reset()
        {
            PatternName = null;
            TitleName = null;
            FactionName = null;
        }
    }
}