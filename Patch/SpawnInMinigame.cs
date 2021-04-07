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

                        Spawn = AddSpawn(Location: new Vector3(-8.808f, 12.710f, 0.013f), name: StringNames.VaultRoom, ImageName: "Vault", Rollover:Spawn[0].Rollover, RolloverSfx: Spawn[0].RolloverSfx, array: Spawn);
                        Spawn = AddSpawn(Location: new Vector3(-19.278f, -1.033f, 0), name: StringNames.Cockpit, ImageName: "Cokpit", Rollover: Spawn[0].Rollover, RolloverSfx: Spawn[0].RolloverSfx, array: Spawn);

                        for (int i = 0; i < Spawn.Length; i++)
                            AirshipSpawn.Logger.LogInfo($"Name : {Spawn[i].Name}, i : {i}");
                        for (int i = 0; i < SpawnPoints.Count; i++)
                            AirshipSpawn.Logger.LogInfo($"i : {SpawnPoints[i]}");

                        if (AirshipSpawn.TypeSpawn.GetValue() == 1)
                            __instance.Locations = new SpawnInMinigame.SpawnLocation[3] { Spawn[7], Spawn[2], Spawn[5] };
                        else if (AirshipSpawn.TypeSpawn.GetValue() == 2)
                            __instance.Locations = new SpawnInMinigame.SpawnLocation[3] { Spawn[SpawnPoints[0]], Spawn[SpawnPoints[1]], Spawn[SpawnPoints[2]] };
                    }

                    return true;
                }
                __instance.Close();
                PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(GetMeetingPosition(PlayerControl.LocalPlayer.PlayerId)) ;
                return false;
            }

            static SpawnInMinigame.SpawnLocation[] AddSpawn(Vector3 Location,StringNames name, string ImageName, AnimationClip Rollover,AudioClip RolloverSfx,SpawnInMinigame.SpawnLocation[] array)
            {
                var Sprite = Utility.HelperSprite.LoadSpriteFromEmbeddedResources($"AirShipSpawnMod.Resource.{ImageName}.png", 306f);

                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = new SpawnInMinigame.SpawnLocation { Location = Location, Name = name, Image = Sprite, Rollover = Rollover, RolloverSfx = RolloverSfx };
                return array;

            }

            public static Vector3 GetMeetingPosition(byte PlayerId) {
                int halfPlayerValue = PlayerId % (PlayerControl.AllPlayerControls.Count / 2);

                Vector3 Position = new Vector3(9.028f, 15.997f, 0);
                if (PlayerId % 2 == 0)
                    Position.y = 14.386f;

                Position.x += 0.728f * halfPlayerValue;

                return Position;
            }
        }
    }
}
