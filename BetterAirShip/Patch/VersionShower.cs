using HarmonyLib;
using TMPro;

namespace BetterAirShip.Patch {

    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    public static class VersionShowerPatch {
        public static void Postfix(VersionShower __instance) {
            Reactor.Patches.ReactorVersionShower.Text.text += "\n<color=#2EADFFFF>Better AirShip</color> by Evan and Hardel";
        }
    }
}