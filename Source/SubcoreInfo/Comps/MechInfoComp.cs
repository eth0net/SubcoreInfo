using Verse;

namespace SubcoreInfo.Comps
{
    /// <summary>
    /// MechInfoComp is added to mechanoids and is used to track the pawn scanned into the subcore.
    /// </summary>
    public class MechInfoComp : BaseInfoComp {
        /// <summary>
        /// Disassembling tracks when a mech is being disassembled.
        /// </summary>
        public bool Disassembling;

        /// <summary>
        /// PostExposeData is used to save our component state.
        /// </summary>
        public override void PostExposeData()
        {
            Scribe_Deep.Look(ref Disassembling, "disassembling");
            base.PostExposeData();
        }

        /// <summary>
        /// Reset allows the component to be reset for reuse.
        /// </summary>
        public new void Reset()
        {
            base.Reset();
            Disassembling = false;
        }
    }
}
