using System.Collections.Generic;
using System.Text;
using Verse;

namespace SubcoreInfo.Comps
{
    /// <summary>
    /// CompPatternInfo implements the common inspect method for pattern components.
    /// </summary>
    public class CompPatternInfo : CompPatternBase
    {
        static TaggedString textName = "Name".Translate();
        static TaggedString textTitle = "Title".Translate();
        static TaggedString textFaction = "Faction".Translate();
        static TaggedString textUnknown = "Unknown".Translate();

        /// <summary>
        /// CompInspectStringExtra adds to the item inspection pane.
        /// </summary>
        /// <returns></returns>
        public override string CompInspectStringExtra()
        {
            StringBuilder sb = new StringBuilder();

            if (SubcoreInfoSettings.showFullName)
            {
                sb.AppendLine(textName + ": " + (PatternName?.ToStringFull ?? textUnknown));
            }
            else
            {
                sb.AppendLine(textName + ": " + (PatternName?.ToStringShort ?? textUnknown));
            }

            if (SubcoreInfoSettings.showTitle)
            {
                sb.AppendLine(textTitle + ": " + (TitleName ?? textUnknown));
            }

            if (SubcoreInfoSettings.showFaction)
            {
                sb.AppendLine(textFaction + ": " + (FactionName ?? textUnknown));
            }

            return sb.ToString().TrimEnd();
        }

        public override IEnumerable<RimWorld.StatDrawEntry> SpecialDisplayStats()
        {
            if (SubcoreInfoSettings.showFullName)
            {
                yield return new RimWorld.StatDrawEntry(StatCategoryDefOf.SubcoreInfo, textName, PatternName?.ToStringFull ?? textUnknown, "The full name of the pawn scanned to make this subcore.", 40);
            }
            else
            {
                yield return new RimWorld.StatDrawEntry(StatCategoryDefOf.SubcoreInfo, textName, PatternName?.ToStringShort ?? textUnknown, "The short name of the pawn scanned to make this subcore.", 30);
            }

            if (SubcoreInfoSettings.showTitle)
            {
                yield return new RimWorld.StatDrawEntry(StatCategoryDefOf.SubcoreInfo, textTitle, TitleName ?? textUnknown, "The title of the pawn scanned to make this subcore.", 20);
            }

            if (SubcoreInfoSettings.showFaction)
            {
                yield return new RimWorld.StatDrawEntry(StatCategoryDefOf.SubcoreInfo, textFaction, FactionName ?? textUnknown, "The faction of the pawn scanned to make this subcore.", 10);
            }
        }
    }
}
