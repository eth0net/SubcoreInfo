using RimWorld;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace SubcoreInfo.Comps
{
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

        /// <summary>
        /// CompInspectStringExtra adds to the item inspection pane.
        /// </summary>
        /// <returns></returns>
        public override string CompInspectStringExtra()
        {
            StringBuilder sb = new();

            if (SubcoreInfoSettings.showTitle && ModsConfig.RoyaltyActive)
            {
                sb.AppendLine(textTitle + ": " + (TitleName ?? textUnknown));
            }

            if (SubcoreInfoSettings.showFullName)
            {
                sb.AppendLine(textName + ": " + (PawnName?.ToStringFull ?? textUnknown));
            }
            else
            {
                sb.AppendLine(textName + ": " + (PawnName?.ToStringShort ?? textUnknown));
            }

            if (SubcoreInfoSettings.showFaction)
            {
                sb.AppendLine(textFaction + ": " + (FactionName ?? textUnknown));
            }

            if (SubcoreInfoSettings.showIdeo && ModsConfig.IdeologyActive)
            {
                sb.AppendLine(textIdeo + ": " + (IdeoName ?? textUnknown));
            }

            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// SpecialDisplayStats adds to the item info pane.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<StatDrawEntry> SpecialDisplayStats()
        {
            if (ModsConfig.RoyaltyActive)
            {
                yield return new(StatCategoryDefOf.SubcoreInfo, textTitle, TitleName ?? textUnknown, "The title of the pawn scanned to make this subcore.", 403);
            }

            yield return new(StatCategoryDefOf.SubcoreInfo, textName, PawnName?.ToStringFull ?? textUnknown, "The full name of the pawn scanned to make this subcore.", 402);

            yield return new(StatCategoryDefOf.SubcoreInfo, textFaction, FactionName ?? textUnknown, "The faction of the pawn scanned to make this subcore.", 401);

            if (ModsConfig.IdeologyActive)
            {
                yield return new(StatCategoryDefOf.SubcoreInfo, textIdeo, IdeoName ?? textUnknown, "The ideoligion of the pawn scanned to make this subcore.", 400);
            }
        }
    }
}
