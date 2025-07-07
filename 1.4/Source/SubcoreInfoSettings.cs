using Verse;

namespace SubcoreInfo;

public class SubcoreInfoSettings : ModSettings
{
    /// <summary>
    /// Show fields for blank subcores in the subcore info panel.
    /// </summary>
    public static bool showBlankSubcores = false;

    /// <summary>
    /// Show unknown fields in the subcore info panel.
    /// </summary>
    public static bool showUnknownFields = false;

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
    /// Separate subcore stacks by stored info.
    /// </summary>
    public static bool separateStacks = true;

    /// <summary>
    /// Generate random info for trader subcores.
    /// </summary>
    public static bool randomTraderInfo = true;

    /// <summary>
    /// Enable the Mr Streamer Special mode.
    /// </summary>
    public static bool mrStreamerSpecial = false;

    /// <summary>
    /// ExposeData saves and loads the settings.
    /// </summary>
    public override void ExposeData()
    {
        Scribe_Values.Look(ref showBlankSubcores, "showBlankSubcores", false);
        Scribe_Values.Look(ref showUnknownFields, "showUnknownFields", false);
        Scribe_Values.Look(ref showTitle, "showTitle", true);
        Scribe_Values.Look(ref showFullName, "showFullName", true);
        Scribe_Values.Look(ref showFaction, "showFaction", true);
        Scribe_Values.Look(ref showIdeo, "showIdeo", true);
        Scribe_Values.Look(ref separateStacks, "separateStacks", true);
        Scribe_Values.Look(ref randomTraderInfo, "randomTraderInfo", true);
        Scribe_Values.Look(ref mrStreamerSpecial, "mrStreamerSpecial", false);
        base.ExposeData();
    }
}
