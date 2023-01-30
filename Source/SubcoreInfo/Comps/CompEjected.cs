using Verse;

namespace SubcoreInfo.Comps
{
    /// <summary>
    /// CompEjected is added to subcore scanners
    /// allowing us to track when a subcore is ejected.
    /// </summary>
    public class CompEjected : ThingComp
    {
        /// <summary>
        /// Ejected tracks whether a subcore has just been ejected.
        /// </summary>
        public bool Ejected = false;

        /// <summary>
        /// PostExposeData is used to save our component state.
        /// </summary>
        public override void PostExposeData()
        {
            Scribe_Values.Look(ref Ejected, "ejected");
            base.PostExposeData();
        }

        /// <summary>
        /// Reset allows the component to be reset for reuse.
        /// </summary>
        public void Reset()
        {
            Ejected = false;
        }
    }
}
