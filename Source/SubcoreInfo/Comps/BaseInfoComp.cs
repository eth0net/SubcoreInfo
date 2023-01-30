using Verse;

namespace SubcoreInfo.Comps
{
    /// <summary>
    /// BaseInfoComp implements the common inspect method for pattern components.
    /// </summary>
    public class BaseInfoComp : ThingComp
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
        /// CompInspectStringExtra adds to the item inspection pane.
        /// </summary>
        /// <returns></returns>
        public override string CompInspectStringExtra()
        {
            return "Pattern".Translate() + ": " + (PatternName?.ToStringShort ?? "Unknown".Translate());
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
