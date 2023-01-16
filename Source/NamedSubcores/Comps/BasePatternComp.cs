using Verse;

namespace NamedSubcores
{
    /// <summary>
    /// BasePatternComp implements the common features for the subcore and mech components.
    /// </summary>
    public class BasePatternComp : ThingComp
    {
        /// <summary>
        /// PatternName tracks the name of the pawn scanned.
        /// </summary>
        public Name PatternName = null;

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
    }
}
