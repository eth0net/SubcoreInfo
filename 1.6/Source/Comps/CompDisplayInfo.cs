using System.Collections.Generic;
using System.Text;
using RimWorld;
using Verse;

namespace SubcoreInfo.Comps;

/// <summary>
///     CompDisplayInfo implements the common display methods for subcore info components.
/// </summary>
public class CompDisplayInfo : CompInfoBase
{
    private static readonly TaggedString TextTitle = "Title".Translate();
    private static readonly TaggedString TextName = "Name".Translate();
    private static readonly TaggedString TextFaction = "Faction".Translate();
    private static readonly TaggedString TextIdeo = "Ideoligion".Translate();
    private static readonly TaggedString TextUnknown = "Unknown".Translate();

    private bool ShowTitle => SubcoreInfoSettings.showTitle && ModsConfig.RoyaltyActive;
    private bool ShowFullName => SubcoreInfoSettings.showFullName;
    private bool ShowFaction => SubcoreInfoSettings.showFaction;
    private bool ShowIdeo => SubcoreInfoSettings.showIdeo && ModsConfig.IdeologyActive;
    private bool ShowUnknown => SubcoreInfoSettings.showUnknownFields;
    private bool ShowBlankSubcores => SubcoreInfoSettings.showBlankSubcores;

    /// <summary>
    ///     CompInspectStringExtra adds to the item inspection pane.
    /// </summary>
    /// <returns></returns>
    public override string CompInspectStringExtra()
    {
        CompInfoBase comp = MrStreamerSpecialUtility.GetDisplayComp(this);
        if (!ShowBlankSubcores && comp.IsBlank)
            return string.Empty;

        StringBuilder sb = new();

        // Title
        if (ShowTitle && (comp.HasTitle || ShowUnknown))
            sb.AppendLine($"{TextTitle}: {comp.TitleName ?? TextUnknown}");

        // Name
        if (ShowFullName && (comp.HasFullName || ShowUnknown))
            sb.AppendLine($"{TextName}: {comp.PawnName?.ToStringFull ?? TextUnknown}");
        else if (!ShowFullName && (comp.HasShortName || ShowUnknown))
            sb.AppendLine($"{TextName}: {comp.PawnName?.ToStringShort ?? TextUnknown}");

        // Faction
        if (ShowFaction && (comp.HasFaction || ShowUnknown))
            sb.AppendLine($"{TextFaction}: {comp.FactionName ?? TextUnknown}");

        // Ideoligion
        if (ShowIdeo && (comp.HasIdeo || ShowUnknown))
            sb.AppendLine($"{TextIdeo}: {comp.IdeoName ?? TextUnknown}");

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    ///     SpecialDisplayStats adds to the item info pane.
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<StatDrawEntry> SpecialDisplayStats()
    {
        CompInfoBase comp = MrStreamerSpecialUtility.GetDisplayComp(this);
        if (!ShowBlankSubcores && comp.IsBlank)
            yield break;

        // Title
        if (ModsConfig.RoyaltyActive && (comp.HasTitle || ShowUnknown))
            yield return new StatDrawEntry(
                StatCategoryDefOf.SubcoreInfo, TextTitle, comp.TitleName ?? TextUnknown,
                "The title of the pawn scanned to make this subcore.", 403
            );

        // Name
        if (comp.HasFullName || ShowUnknown)
            yield return new StatDrawEntry(
                StatCategoryDefOf.SubcoreInfo, TextName, comp.PawnName?.ToStringFull ?? TextUnknown,
                "The full name of the pawn scanned to make this subcore.", 402
            );

        // Faction
        if (comp.HasFaction || ShowUnknown)
            yield return new StatDrawEntry(
                StatCategoryDefOf.SubcoreInfo, TextFaction, comp.FactionName ?? TextUnknown,
                "The faction of the pawn scanned to make this subcore.", 401
            );

        // Ideoligion
        if (ModsConfig.IdeologyActive && (comp.HasIdeo || ShowUnknown))
            yield return new StatDrawEntry(
                StatCategoryDefOf.SubcoreInfo, TextIdeo, comp.IdeoName ?? TextUnknown,
                "The ideoligion of the pawn scanned to make this subcore.", 400
            );
    }
}
