using Verse;

namespace NamedSubcores
{
    /// <summary>
    /// SubcoreScannerComp is added to subcore scanners and allows us to track when a subcore is ejected.
    /// </summary>
    public class SubcoreScannerComp : ThingComp
    {
        /// <summary>
        /// Ejected tracks whether a subcore has just been ejected.
        /// </summary>
        public bool Ejected = false;

        /// <summary>
        /// PatternName tracks the name of the pawn in the scanner.
        /// </summary>
        public Name OccupantName;

        /// <summary>
        /// PostExposeData is used to save our component state.
        /// </summary>
        public override void PostExposeData()
        {
            Scribe_Values.Look(ref Ejected, "ejected");
            Scribe_Deep.Look(ref OccupantName, "occupantName");
            base.PostExposeData();
        }

        /// <summary>
        /// Reset allows the component to be reset for reuse.
        /// </summary>
        public void Reset()
        {
            Ejected = false;
            OccupantName = null;
        }
    }
}
