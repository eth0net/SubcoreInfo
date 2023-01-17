using Verse;

namespace NamedSubcores
{
    /// <summary>
    /// InspectPatternComp implements the common inspect method for pattern components.
    /// </summary>
    public class InspectPatternComp : BasePatternComp
    {
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
