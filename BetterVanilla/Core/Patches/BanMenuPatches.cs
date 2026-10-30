using HarmonyLib;

namespace BetterVanilla.Core.Patches;

[HarmonyPatch(typeof(BanMenu))]
internal static class BanMenuPatches
{
    [HarmonyPrefix, HarmonyPatch(nameof(BanMenu.SetVisible))]
    public static bool SetVisiblePrefix(BanMenu __instance, bool show)
    {
        if (!AmongUsClient.Instance.AmHost) return true;
        show &= PlayerControl.LocalPlayer && PlayerControl.LocalPlayer.Data != null;
        __instance.BanButton.gameObject.SetActive(AmongUsClient.Instance.CanBan());
        __instance.KickButton.gameObject.SetActive(AmongUsClient.Instance.CanKick());
        __instance.MenuButton.gameObject.SetActive(show);
        return false;
    }
}