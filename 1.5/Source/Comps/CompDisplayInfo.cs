using RimWorld;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;

namespace SubcoreInfo.Comps;

/// <summary>
/// CompDisplayInfo implements the common display methods for subcore info components.
/// </summary>
public class CompDisplayInfo : CompInfoBase
{
    static TaggedString textTitle = "Title".Translate();
    static TaggedString textName = "Name".Translate();
    static TaggedString textFaction = "Faction".Translate();
    static TaggedString textIdeo = "Ideoligion".Translate();
    static TaggedString textUnknown = "Unknown".Translate();

    private CompInfoBase specialComp;
    private int lastFrame;

    /// <summary>
    /// CompInspectStringExtra adds to the item inspection pane.
    /// </summary>
    /// <returns></returns>
    public override string CompInspectStringExtra()
    {
        UpdateSpecialComp();

        CompInfoBase comp = specialComp ?? this;

        StringBuilder sb = new();

        if (SubcoreInfoSettings.showTitle && ModsConfig.RoyaltyActive)
        {
            sb.AppendLine(textTitle + ": " + (comp.TitleName ?? textUnknown));
        }

        if (SubcoreInfoSettings.showFullName)
        {
            sb.AppendLine(textName + ": " + (comp.PawnName?.ToStringFull ?? textUnknown));
        }
        else
        {
            sb.AppendLine(textName + ": " + (comp.PawnName?.ToStringShort ?? textUnknown));
        }

        if (SubcoreInfoSettings.showFaction)
        {
            sb.AppendLine(textFaction + ": " + (comp.FactionName ?? textUnknown));
        }

        if (SubcoreInfoSettings.showIdeo && ModsConfig.IdeologyActive)
        {
            sb.AppendLine(textIdeo + ": " + (comp.IdeoName ?? textUnknown));
        }

        return sb.ToString().TrimEnd();
    }


    /// <summary>
    /// SpecialDisplayStats adds to the item info pane.
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<StatDrawEntry> SpecialDisplayStats()
    {
        UpdateSpecialComp();

        CompInfoBase comp = specialComp ?? this;

        if (ModsConfig.RoyaltyActive)
        {
            yield return new(StatCategoryDefOf.SubcoreInfo, textTitle, comp.TitleName ?? textUnknown, "The title of the pawn scanned to make this subcore.", 403);
        }

        yield return new(StatCategoryDefOf.SubcoreInfo, textName, comp.PawnName?.ToStringFull ?? textUnknown, "The full name of the pawn scanned to make this subcore.", 402);

        yield return new(StatCategoryDefOf.SubcoreInfo, textFaction, comp.FactionName ?? textUnknown, "The faction of the pawn scanned to make this subcore.", 401);

        if (ModsConfig.IdeologyActive)
        {
            yield return new(StatCategoryDefOf.SubcoreInfo, textIdeo, comp.IdeoName ?? textUnknown, "The ideoligion of the pawn scanned to make this subcore.", 400);
        }
    }

    private void UpdateSpecialComp()
    {
        if (MrStreamerSpecialUtility.Enabled)
        {
            int newFrame = Time.frameCount;

            if (newFrame - lastFrame >= 5)
            {
                if (specialComp == null && Rand.Bool)
                {
                    specialComp ??= MrStreamerSpecialUtility.RandomSubcore;
                }
                else if (specialComp != null)
                {
                    specialComp = null;
                }
            }

            lastFrame = newFrame;
        }
        else if (specialComp != null)
        {
            specialComp = null;
        }
    }
}
