using HarmonyLib;
using Reactor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BetterAirShip.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    class TeleportationMeeting {

        public static bool TeleportationStarted = false;

        public static void Prefix(PlayerControl __instance) {
            if (BetterAirShip.Teleportation.GetValue()) {
                if (!TeleportationStarted && Vector2.Distance(__instance.transform.position, new Vector2(17.331f, 15.236f)) < 0.5f && UnityEngine.Object.FindObjectOfType<AirshipStatus>() != null)
                Coroutines.Start(CoTeleportPlayer(__instance));
            }
        }

/*        private static IEnumerator BlackScreenFade(float Duration, bool fadeout) {
            float elapsedTime = 0;
            float alpha = fadeout ? 0f : 1f;

            DestroyableSingleton<HudManager>.Instance.FullScreen.enabled = true;
            DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0f, 0f, 0f, fadeout ? 0f : 1f);

            while (elapsedTime < Duration) {
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            while (fadeout ? alpha < 1 : alpha > 0) {
                if (fadeout) alpha += (5 * Time.deltaTime);
                else alpha -= (5 * Time.deltaTime);

                DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0f, 0f, 0f, alpha);
                yield return new WaitForEndOfFrame();
            }

            HudManager.Instance.FullScreen.enabled = false;
            yield break;
        }*/

        private static IEnumerator Fade(bool fadeAway, bool enableAfterFade) {
            DestroyableSingleton<HudManager>.Instance.FullScreen.enabled = true;

            if (fadeAway) {
                for (float i = 1; i >= 0; i -= 1 / Time.deltaTime) {
                    DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0, 0, 0, i);
                    yield return null;
                }
            }
            else {
                for (float i = 0; i <= 1; i += 1 / Time.deltaTime) {
                    DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0, 0, 0, i);
                    yield return null;
                }
            }

            if (enableAfterFade)
                HudManager.Instance.FullScreen.enabled = false;
        }

        private static IEnumerator CoTeleportPlayer(PlayerControl instance) {
            TeleportationStarted = true;
            yield return Fade(false, false);
            instance.NetTransform.RpcSnapTo(new Vector2(5.753f, -10.011f));
            yield return Fade(true, true);
            TeleportationStarted = false;

            yield break;
        }
    }
}
