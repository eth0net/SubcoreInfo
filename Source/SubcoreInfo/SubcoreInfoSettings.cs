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
        /// ExposeData saves and loads the settings.
        /// </summary>
        public override void ExposeData()
        {
            Scribe_Values.Look(ref separatePatternStacks, "separatePatternStacks", true, true);
            Scribe_Values.Look(ref randomTraderPatterns, "randomTraderPatterns", true, true);
            base.ExposeData();
        }
    }
}
