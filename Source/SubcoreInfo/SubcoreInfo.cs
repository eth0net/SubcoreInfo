using RimWorld;
using UnityEngine;
using Verse;

namespace SubcoreInfo
{
    /// <summary>
    /// SubcoreInfo static class to load up the mod and initialise everything.
    /// </summary>
    public class SubcoreInfo : Mod
    {
        /// <summary>
        /// SubcoreInfo settings reference.
        /// </summary>
        internal static SubcoreInfoSettings settings;

        /// <summary>
        /// SubcoreInfo constructor to patch things using harmony.
        /// </summary>
        public SubcoreInfo(ModContentPack content) : base(content)
        {
            settings = GetSettings<SubcoreInfoSettings>();
            new HarmonyLib.Harmony("eth0net.SubcoreInfo").PatchAll();
        }

        /// <summary>
        /// DoSettingsWindowContents configures the settings window.
        /// </summary>
        /// <param name="inRect"></param>
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new();

            listing.Begin(inRect);

            listing.CheckboxLabeled("Separate subcore stacks by pattern", ref SubcoreInfoSettings.separatePatternStacks);
            listing.CheckboxLabeled("Random patterns on trader subcores", ref SubcoreInfoSettings.randomTraderPatterns);

            listing.End();

            base.DoSettingsWindowContents(inRect);
        }

        /// <summary>
        /// SettingsCategory returns the name of the settings category.
        /// </summary>
        /// <returns></returns>
        public override string SettingsCategory()
        {
            return "SubcoreInfo".Translate();
        }
    }
}
