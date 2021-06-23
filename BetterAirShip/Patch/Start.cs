using HarmonyLib;

namespace BetterAirShip.Patch {
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    class GameEndedPatch {
        public static void Postfix(ShipStatus __instance) {
            SpawnInMinigamePatch.GameStarted = false;
        }
    }

    [HarmonyPatch(typeof(HeliSabotageSystem), nameof(HeliSabotageSystem.UpdateHeliSize))]
    class HeliCountDown {
        public static void Prefix(HeliSabotageSystem __instance) {
            if (__instance.Countdown > BetterAirShip.CrashCourseTime.GetValue())
                __instance.Countdown = BetterAirShip.CrashCourseTime.GetValue();
        }
    }
}
