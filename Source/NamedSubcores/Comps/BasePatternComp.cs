using Verse;

namespace NamedSubcores
{
    /// <summary>
    /// BasePatternComp implements the common features for the pattern components.
    /// </summary>
    public class BasePatternComp : ThingComp
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

        /// <summary>
        /// Reset allows the component to be reset for reuse.
        /// </summary>
        public void Reset()
        {
            PatternName = null;
        }
    }
}
