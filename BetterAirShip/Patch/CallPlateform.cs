using BetterAirShip.Systems;
using HarmonyLib;
using System.Linq;
using UnityEngine;

namespace BetterAirShip.Patch {

    [HarmonyPatch(typeof(AirshipStatus), nameof(AirshipStatus.OnEnable))]
    public static class CallPlateform {
        public static void Postfix(AirshipStatus __instance) {
            Tasks.CreateThisTask(new Vector3(5.531f, 9.788f, 1f));

            /*            GameObject CallPlateform = new GameObject("Call Plateform");
                        CallPlateform.transform.position = new Vector3(5.531f, 9.788f, 1f);
                        CallPlateform.transform.localScale = new Vector3(1f, 1f, 1f);
                        CallPlateform.SetActive(true);

                        // Visual
                        SpriteRenderer renderer = CallPlateform.AddComponent<SpriteRenderer>();
                        renderer.sprite = ResourceLoader.TaskSprite;
                        renderer.material = new Material(Shader.Find("Sprites/Outline"));
                        CallPlateform.layer = 12;

                        // Console
                        Console console = CallPlateform.AddComponent<Console>();
                        console.ConsoleId = 0;
                        console.AllowImpostor = true;
                        console.GhostsIgnored = false;
                        console.checkWalls = false;
                        console.Image = renderer;
                        console.onlyFromBelow = true;
                        console.onlySameRoom = false;
                        console.usableDistance = 2f;
                        console.Room = SystemTypes.MeetingRoom;

                        var allConsoles = __instance.AllConsoles.ToList();
                        allConsoles.Add(console);
                        __instance.AllConsoles = allConsoles.ToArray();*/
        }
    }
}   