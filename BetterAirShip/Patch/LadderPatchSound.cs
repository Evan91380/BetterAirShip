/*using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BetterAirShip.Patch {

    [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics._CoClimbLadder_d__33))]
    class LadderPatchSound {

        public static IEnumerator Prefix(PlayerPhysics __instance, [HarmonyArgument(0)] Ladder source, [HarmonyArgument(1)] byte climbLadderSid) {
            __instance.myPlayer.Collider.enabled = false;
            __instance.myPlayer.moveable = false;
            __instance.myPlayer.NetTransform.enabled = false;

            if (__instance.myPlayer.AmOwner) {
                __instance.myPlayer.MyPhysics.inputHandler.enabled = true;
            }

            yield return __instance.WalkPlayerTo(source.transform.position, 0.001f, 1f);
            yield return Effects.Wait(0.1f);
            __instance.myPlayer.FootSteps.clip = source.UseSound;
            __instance.myPlayer.FootSteps.loop = true;
            __instance.myPlayer.FootSteps.Play();
            yield return __instance.WalkPlayerTo(source.Destination.transform.position, 0.001f, (float) (source.IsTop ? 2 : 1));
            __instance.myPlayer.CurrentPet.transform.position = __instance.myPlayer.transform.position;
            __instance.ResetAnimState();
            yield return Effects.Wait(0.1f);
            __instance.myPlayer.Collider.enabled = true;
            __instance.myPlayer.moveable = true;
            __instance.myPlayer.NetTransform.enabled = true;
            yield break;
        }
    }
}
*/