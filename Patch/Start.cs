using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirShipSpawn.Patch
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    class GameEndedPatch
    {
        public static void Postfix(ShipStatus __instance)
        {
            SpawnInMinigamePatch.GameStarted = false;
        }
    }
}
