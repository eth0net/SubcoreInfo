using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace NamedSubcores
{
    public class NamedSubcoreComp : ThingComp
    {
        public Name PawnName;

        public override void PostExposeData()
        {
            Scribe_Deep.Look(ref PawnName, "pawnName");
            base.PostExposeData();
        }
    }
}
