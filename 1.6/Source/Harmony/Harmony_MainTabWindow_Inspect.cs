using HarmonyLib;
using RimWorld;

namespace SubcoreInfo.Harmony
{
    [HarmonyPatch(typeof(MainTabWindow_Inspect), nameof(MainTabWindow_Inspect.DoWindowContents))]
    public static class Harmony_MainTabWindow_Inspect_DoWindowContents
    {
        private static bool wasOpen = false;

        public static void Prefix(MainTabWindow_Inspect __instance)
        {
            bool isOpen = __instance.AnythingSelected && __instance.ShouldShowPaneContents;
            if (!isOpen && wasOpen)
            {
                MrStreamerSpecialUtility.NotifyInspectPaneClosed();
                wasOpen = false;
            }
            else if (isOpen && !wasOpen)
            {
                MrStreamerSpecialUtility.NotifyInspectPaneOpened();
                wasOpen = true;
            }
        }
    }
}
