using Verse;

namespace NamedSubcores
{
    /// <summary>
    /// MechPatternComp is added to subcores and is used to track the pawn scanned into the subcore.
    /// </summary>
    public class SubcorePatternComp : InspectPatternComp {
        /// <summary>
        /// AllowStackWith ensures that subcores can only be stacked with others of the same pattern.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool AllowStackWith(Thing other)
        {
            if (base.AllowStackWith(other) == false) { return false; };

            SubcorePatternComp otherComp = other?.TryGetComp<SubcorePatternComp>();
            if (otherComp == null) { return false; }

            return PatternName == otherComp.PatternName;
        }
    }
}
