using Verse;

namespace SubcoreInfo
{
    internal class SubcoreInfoSettings : ModSettings
    {
        /// <summary>
        /// Separate subcore stacks by pattern.
        /// </summary>
        public static bool separatePatternStacks = true;

        /// <summary>
        /// Generate random patterns for trader subcores.
        /// </summary>
        public static bool randomTraderPatterns = true;

        /// <summary>
        /// Show pawn title in the subcore info panel.
        /// </summary>
        public static bool showTitle = true;

        /// <summary>
        /// Show pawn full name in the subcore info oanel.
        /// </summary>
        public static bool showFullName = true;

        /// <summary>
        /// Show pawn faction in the subcore info panel.
        /// </summary>
        public static bool showFaction = true;

        /// <summary>
        /// Show pawn ideoligion in the subcore info panel.
        /// </summary>
        public static bool showIdeo = true;

        /// <summary>
        /// ExposeData saves and loads the settings.
        /// </summary>
        public override void ExposeData()
        {
            Scribe_Values.Look(ref separatePatternStacks, "separatePatternStacks", true);
            Scribe_Values.Look(ref randomTraderPatterns, "randomTraderPatterns", true);
            Scribe_Values.Look(ref showTitle, "showTitle", true);
            Scribe_Values.Look(ref showFullName, "showFullName", true);
            Scribe_Values.Look(ref showFaction, "showFaction", true);
            Scribe_Values.Look(ref showIdeo, "showIdeo", true);
            base.ExposeData();
        }
    }
}
