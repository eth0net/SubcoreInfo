using Verse;

namespace NamedSubcores
{
    /// <summary>
    /// MechGestatorComp is added to mech gestators and allows us to track the subcore component during gestation.
    /// </summary>
    public class MechGestatorComp : ThingComp
    {
        /// <summary>
        /// SubcoreComp tracks the subcore component.
        /// </summary>
        public SubcorePatternComp SubcoreComp;

        /// <summary>
        /// PostExposeData is used to save our component state.
        /// </summary>
        public override void PostExposeData()
        {
            Scribe_Deep.Look(ref SubcoreComp, "subcoreComp");
            base.PostExposeData();
        }

        /// <summary>
        /// Reset allows the component to be reset for reuse.
        /// </summary>
        public void Reset()
        {
            SubcoreComp = null;
        }
    }
}
