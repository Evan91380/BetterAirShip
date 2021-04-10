using HarmonyLib;

namespace BetterAirShip.Patch {

    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    public static class VersionShowerPatch {
        public static void Postfix(VersionShower __instance) {
            Reactor.Patches.ReactorVersionShower.Text.Text += "\n[0015CCFF]Better AirShip[] by Evan and Hardel";
        }
    }
}