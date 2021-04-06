using HarmonyLib;

namespace AirShipSpawn.Patch {

    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    public static class VersionShowerPatch {
        public static void Postfix(VersionShower __instance) {
            Reactor.Patches.ReactorVersionShower.Text.Text += "\n[2EADFFFF]AirShip Spawn[] by Evan";
        }
    }
}