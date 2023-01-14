using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using UnityEngine.Assertions.Must;
using Verse;

namespace NamedSubcores
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("eth0net.NamedSubcores.harmony");
            harmony.PatchAll();
        }

        static bool ejected = false;

        [HarmonyPatch(typeof(Building_SubcoreScanner), "EjectContents")]
        internal class Harmony_Building_SubcoreScanner_EjectContents
        {
            internal static void Postfix()
            {
                Log.Message("Ejected, looking for subcore");
                ejected = true;
            }
        }

        [HarmonyPatch(typeof(Building_SubcoreScanner), "Tick")]
        internal class Harmony_Building_SubcoreScanner_Tick
        {
            internal static Name pawnName;

            // Store the name of the scanned pawn
            internal static void Prefix(Building_SubcoreScanner __instance)
            {
                pawnName = __instance?.Occupant?.Name ?? null;
            }

            // Try to track down the new subcore, then store the pawn name
            internal static void Postfix(Building_SubcoreScanner __instance)
            {
                if (pawnName != null && ejected)
                {
                    List<IntVec3> cells = __instance.InteractionCells;
                    cells.ForEach(cell =>
                    {
                        List<Thing> things = cell.GetThingList(__instance.Map);
                        things.ForEach(thing =>
                        {
                            if ((thing.def.defName == "SubcoreRegular" && __instance.def.defName == "SubcoreSoftscanner") || (thing.def.defName == "SubcoreHigh" && __instance.def.defName == "SubcoreRipscanner"))
                            {
                                NamedSubcoreComp comp = thing.TryGetComp<NamedSubcoreComp>();
                                if (comp != null && comp.PawnName == null)
                                {
                                    comp.PawnName = pawnName;
                                    pawnName = null;
                                    ejected = false;
                                    return;
                                }
                            }
                        });
                    });
                }
            }
        }
    }
}
