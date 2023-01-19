﻿using Verse;

namespace SubcoreInfo
{
    /// <summary>
    /// SubcoreScannerPatternComp is added to subcore scanners
    /// allowing us to track when a subcore is ejected.
    /// </summary>
    public class SubcoreScannerPatternComp : BasePatternComp
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
        public new void Reset()
        {
            base.Reset();
            Ejected = false;
        }
    }
}