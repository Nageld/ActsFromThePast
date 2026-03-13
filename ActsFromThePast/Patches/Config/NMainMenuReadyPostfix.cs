using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;

namespace ActsFromThePast.Patches.Config;

[HarmonyPatch(typeof(NMainMenu), "_Ready")]
public static class NMainMenuReadyPostfix
{
    public static void Postfix()
    {
        AftpConfigManager.GetConfig();
    }
}