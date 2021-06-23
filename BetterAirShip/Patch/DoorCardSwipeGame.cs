using HarmonyLib;

namespace BetterAirShip.Patch {
    [HarmonyPatch(typeof(DoorCardSwipeGame), nameof(DoorCardSwipeGame.Begin))]
    class DoorSwipePatch {
        static void Prefix(DoorCardSwipeGame __instance) {
            __instance.minAcceptedTime = BetterAirShip.minTimeDoor.GetValue();
        }
    }
}