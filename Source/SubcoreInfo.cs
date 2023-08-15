using RimWorld;
using UnityEngine;
using Verse;

namespace SubcoreInfo;

/// <summary>
/// SubcoreInfo class to load up the mod and initialise everything.
/// </summary>
public class SubcoreInfo : Mod
{
    /// <summary>
    /// SubcoreInfo settings reference.
    /// </summary>
    internal static SubcoreInfoSettings settings;

    /// <summary>
    /// SubcoreInfo constructor to load the mod and settings.
    /// Also applies patches using harmony.
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

        listing.GapLine();
        listing.Label("Interface Settings");
        listing.GapLine();

        if (ModsConfig.RoyaltyActive)
        {
            listing.CheckboxLabeled("Show pawn title", ref SubcoreInfoSettings.showTitle);
        }
        listing.CheckboxLabeled("Show pawn full name", ref SubcoreInfoSettings.showFullName);
        listing.CheckboxLabeled("Show pawn faction", ref SubcoreInfoSettings.showFaction);
        if (ModsConfig.IdeologyActive)
        {
            listing.CheckboxLabeled("Show pawn ideoligion", ref SubcoreInfoSettings.showIdeo);
        }

        listing.GapLine();
        listing.Label("Misc Settings");
        listing.GapLine();

        listing.CheckboxLabeled("Separate subcore stacks by pattern", ref SubcoreInfoSettings.separateStacks);
        listing.CheckboxLabeled("Random patterns on trader subcores", ref SubcoreInfoSettings.randomTraderInfo);

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
