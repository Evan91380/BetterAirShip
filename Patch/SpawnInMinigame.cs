using HarmonyLib;
using Hazel;
using PowerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AirShipSpawn.Patch {

    class SpawnInMinigamePatch {
        public static bool GameStarted = false;
        public static List<byte> SpawnPoints = new List<byte>();

        [HarmonyPatch(typeof(SpawnInMinigame), nameof(SpawnInMinigame.Begin))] 
        class SpawnInMiningameBeginPatch{
            static bool Prefix(SpawnInMinigame __instance) {

                //respawn a true || GameStarted == false
                if (AirshipSpawn.MeetingRespawn.GetValue() || !GameStarted) { // respawn a false et GameStarted ==  true
                    GameStarted = true;
                    if(AirshipSpawn.TypeSpawn.GetValue() != 0){
                        var Spawn = __instance.Locations.ToArray<SpawnInMinigame.SpawnLocation>();

                        for (int i = 0; i < Spawn.Length; i++)
                            AirshipSpawn.Logger.LogInfo($"Name : {Spawn[i].Name}, i : ${i}");
                        for (int i = 0; i < SpawnPoints.Count; i++)
                            AirshipSpawn.Logger.LogInfo($"i : {SpawnPoints[i]}");

                        if (AirshipSpawn.TypeSpawn.GetValue() == 1)
                            __instance.Locations = new SpawnInMinigame.SpawnLocation[3] { Spawn[3], Spawn[2], Spawn[5] };
                        else if (AirshipSpawn.TypeSpawn.GetValue() == 2)
                            __instance.Locations = new SpawnInMinigame.SpawnLocation[3] { Spawn[SpawnPoints[0]], Spawn[SpawnPoints[1]], Spawn[SpawnPoints[2]] };
                    }

                    return true;
                }
                __instance.Close();
                PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(new Vector3(7.674f, 15.207f, 0.015f));
                return false;
            }
        }
    }
}
