using HarmonyLib;
using Verse;

namespace NamedSubcores
{
    /// <summary>
    /// NamedSubcores static class to load up the mod and initialise everything
    /// </summary>
    [StaticConstructorOnStartup]
    public static class NamedSubcores
    {
        /// <summary>
        /// NamedSubcores constructor to patch things using harmony
        /// </summary>
        static NamedSubcores()
        {
            var harmony = new Harmony("eth0net.NamedSubcores.harmony");
            harmony.PatchAll();
        }
    }
}
