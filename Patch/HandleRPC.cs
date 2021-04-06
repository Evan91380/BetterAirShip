using HarmonyLib;
using Hazel;
using System.Collections.Generic;
using AirShipSpawn.Utility;
using System.Linq;
using UnityEngine;

namespace AirShipSpawn.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class HandleRpcPatch {

        public static bool Prefix([HarmonyArgument(0)] byte CallId, [HarmonyArgument(1)] MessageReader reader) {

            if (CallId == (byte)CustomRPC.SetSpawn) {
                List<byte> spawnPoints = reader.ReadBytesAndSize().ToList();
                SpawnInMinigamePatch.SpawnPoints = spawnPoints;

                return false;
            }

            return true;
        }
    }
}