using HarmonyLib;

namespace BetterAirShip.Patch {
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    class GameEndedPatch {
        public static void Postfix(ShipStatus __instance) {
            SpawnInMinigamePatch.GameStarted = false;
        }
    }
}
