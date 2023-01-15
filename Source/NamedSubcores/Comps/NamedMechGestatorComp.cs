using Verse;

namespace NamedSubcores
{
    /// <summary>
    /// NamedMechGestatorComp is added to Building_MechGestator,
    /// allowing us to track subcore occupants during gestation.
    /// </summary>
    public class NamedMechGestatorComp : ThingComp
    {
        /// <summary>
        /// SubcoreOccupantName tracks the name of subcore occupant.
        /// </summary>
        public Name SubcoreOccupantName;

        /// <summary>
        /// PostExposeData is used to save our component state.
        /// </summary>
        public override void PostExposeData()
        {
            Scribe_Deep.Look(ref SubcoreOccupantName, "subcoreOccupantName");
            base.PostExposeData();
        }
    }
}
