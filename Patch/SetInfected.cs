﻿using AirShipSpawn;
using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirShipSpawn.Patch {
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    class SetInfectedPatch {
        public static void Postfix() {
            Random random = new Random(); 
            List<byte> randomList = new List<byte>();
            byte MyNumber = 0;

            MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetSpawn, SendOption.None, -1);

            randomList = new List<byte>();

            while (randomList.Count < 3) {
                MyNumber = (byte) random.Next(0, AirshipSpawn.NewSpawn.GetValue() ? 8 : 6);
                if (!randomList.Contains(MyNumber))
                    randomList.Add(MyNumber);
            }


            messageWriter.WriteBytesAndSize(randomList.ToArray());
            AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
            SpawnInMinigamePatch.SpawnPoints = randomList;
        }
    }
}