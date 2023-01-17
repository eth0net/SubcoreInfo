using Verse;

namespace SubcoreInfo
{
    /// <summary>
    /// SubcoreInfoComp is added to subcores and is used to track the pawn scanned into the subcore.
    /// </summary>
    public class SubcoreInfoComp : BaseInfoComp
    {
        /// <summary>
        /// AllowStackWith ensures that subcores can only be stacked with others of the same pattern.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool AllowStackWith(Thing other)
        {
            if (base.AllowStackWith(other) == false) { return false; };

            SubcoreInfoComp otherComp = other?.TryGetComp<SubcoreInfoComp>();
            if (otherComp == null) { return false; }

            return PatternName == otherComp.PatternName;
        }
    }
}
