using HarmonyLib;
using Verse;

namespace SubcoreInfo
{
    /// <summary>
    /// SubcoreInfo static class to load up the mod and initialise everything.
    /// </summary>
    [StaticConstructorOnStartup]
    public static class SubcoreInfo
    {
        /// <summary>
        /// SubcoreInfo constructor to patch things using harmony.
        /// </summary>
        static SubcoreInfo()
        {
            var harmony = new Harmony("eth0net.SubcoreInfo.harmony");
            harmony.PatchAll();
        }
    }
}
