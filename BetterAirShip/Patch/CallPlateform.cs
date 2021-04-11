using BetterAirShip.Systems;
using HarmonyLib;
using Hazel;
using Reactor;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace BetterAirShip.Patch {

    [HarmonyPatch(typeof(AirshipStatus), nameof(AirshipStatus.OnEnable))]
    public static class CallPlateform {

        public static bool PlateformIsUsed = false;

        public static void Postfix(AirshipStatus __instance) {
            Tasks.AllCustomPlateform.Clear();
            Tasks.NearestTask = null;

            if (BetterAirShip.CallPlateform.GetValue()) {
                Tasks.CreateThisTask(new Vector3(5.531f, 9.788f, 1f), new Vector3(0f, 0f, 0f), () => {
                    var Plateform = Object.FindObjectOfType<MovingPlatformBehaviour>();

                    if (!Plateform.IsLeft && !PlateformIsUsed)
                        UsePlateforRpc(Plateform, false);
                });

                Tasks.CreateThisTask(new Vector3(10.148f, 9.806f, 1f), new Vector3(0f, 180f, 0f), () => {
                    var Plateform = Object.FindObjectOfType<MovingPlatformBehaviour>();

                    if (Plateform.IsLeft && !PlateformIsUsed)
                        UsePlateforRpc(Plateform, true);
                });
            }
        }

        public static void SyncPlateform(bool isLeft) {
            var Plateform = Object.FindObjectOfType<MovingPlatformBehaviour>();
            Coroutines.Start(UsePlatform(Plateform, isLeft));
        }

        private static void UsePlateforRpc(MovingPlatformBehaviour Plateform, bool isLeft) {
            MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SyncPlateform, SendOption.None, -1);
            messageWriter.Write(isLeft);
            AmongUsClient.Instance.FinishRpcImmediately(messageWriter);

            Coroutines.Start(UsePlatform(Plateform, isLeft));
        }

        private static IEnumerator UsePlatform(MovingPlatformBehaviour Plateform, bool isLeft) {
            PlateformIsUsed = true;
            Plateform.IsLeft = isLeft;
            Plateform.transform.localPosition = (Plateform.IsLeft ? Plateform.LeftPosition : Plateform.RightPosition);
            Plateform.IsDirty = true;

            Vector3 sourcePos = Plateform.IsLeft ? Plateform.LeftPosition : Plateform.RightPosition;
            Vector3 targetPos = (!Plateform.IsLeft) ? Plateform.LeftPosition : Plateform.RightPosition;
            yield return Effects.Wait(0.1f);

            yield return Effects.Slide3D(Plateform.transform, sourcePos, targetPos, PlayerControl.LocalPlayer.MyPhysics.Speed);

            Plateform.IsLeft = !Plateform.IsLeft;
			yield return Effects.Wait(0.1f);
            PlateformIsUsed = false;

			yield break;
        }
	}
}