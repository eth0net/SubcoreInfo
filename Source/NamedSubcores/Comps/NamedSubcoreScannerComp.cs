using Verse;

namespace NamedSubcores
{
    /// <summary>
    /// NamedSubcoreScannerComp is added to Building_SubcoreScanner,
    /// allowing us to track when a subcore is ejected from the scanner.
    /// </summary>
    public class NamedSubcoreScannerComp : ThingComp
    {
        /// <summary>
        /// Ejected tracks whether a subcore has just been ejected.
        /// </summary>
        public bool Ejected = false;

        /// <summary>
        /// OccupantName tracks the name of the pawn in the scanner.
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
    }
}
