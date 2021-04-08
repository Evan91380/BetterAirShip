using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirShipSpawn.Patch
{
    [HarmonyPatch(typeof(DoorCardSwipeGame), nameof(DoorCardSwipeGame.Begin))]
    class DoorSwipePatch
    {
        static void Prefix(DoorCardSwipeGame __instance)
        {
            __instance.minAcceptedTime = AirshipSpawn.minTimeDoor.GetValue();
        }
    }
}
