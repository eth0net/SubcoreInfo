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

    /// <summary>
    ///     CompInspectStringExtra adds to the item inspection pane.
    /// </summary>
    /// <returns></returns>
    public override string CompInspectStringExtra()
    {
        CompInfoBase comp = MrStreamerSpecialUtility.GetDisplayComp(this);
        StringBuilder sb = new();
        if (SubcoreInfoSettings.showTitle && ModsConfig.RoyaltyActive)
            sb.AppendLine(TextTitle + ": " + (comp.TitleName ?? TextUnknown));
        if (SubcoreInfoSettings.showFullName)
            sb.AppendLine(TextName + ": " + (comp.PawnName?.ToStringFull ?? TextUnknown));
        else
            sb.AppendLine(TextName + ": " + (comp.PawnName?.ToStringShort ?? TextUnknown));
        if (SubcoreInfoSettings.showFaction) sb.AppendLine(TextFaction + ": " + (comp.FactionName ?? TextUnknown));
        if (SubcoreInfoSettings.showIdeo && ModsConfig.IdeologyActive)
            sb.AppendLine(TextIdeo + ": " + (comp.IdeoName ?? TextUnknown));
        return sb.ToString().TrimEnd();
    }

    /// <summary>
    ///     SpecialDisplayStats adds to the item info pane.
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<StatDrawEntry> SpecialDisplayStats()
    {
        CompInfoBase comp = MrStreamerSpecialUtility.GetDisplayComp(this);
        if (ModsConfig.RoyaltyActive)
            yield return new StatDrawEntry(
                StatCategoryDefOf.SubcoreInfo, TextTitle, comp.TitleName ?? TextUnknown,
                "The title of the pawn scanned to make this subcore.", 403
            );
        yield return new StatDrawEntry(
            StatCategoryDefOf.SubcoreInfo, TextName, comp.PawnName?.ToStringFull ?? TextUnknown,
            "The full name of the pawn scanned to make this subcore.", 402
        );
        yield return new StatDrawEntry(
            StatCategoryDefOf.SubcoreInfo, TextFaction, comp.FactionName ?? TextUnknown,
            "The faction of the pawn scanned to make this subcore.", 401
        );
        if (ModsConfig.IdeologyActive)
            yield return new StatDrawEntry(
                StatCategoryDefOf.SubcoreInfo, TextIdeo, comp.IdeoName ?? TextUnknown,
                "The ideoligion of the pawn scanned to make this subcore.", 400
            );
    }
}
