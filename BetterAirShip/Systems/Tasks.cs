using System;
using System.Collections.Generic;
using HarmonyLib;
using Reactor;
using UnityEngine;

namespace BetterAirShip.Systems {

    [RegisterInIl2Cpp]
    class Tasks : MonoBehaviour {
        public Tasks(IntPtr ptr) : base(ptr) { }

		public static List<GameObject> AllCustomPlateform = new List<GameObject>();
		public static Tasks NearestTask = null;
		private SpriteRenderer renderer;
		private BoxCollider2D collider;
		public float UsableDistance = 1f;
		public byte Id;

		private void Awake() {
			renderer = gameObject.AddComponent<SpriteRenderer>();
			renderer.material = new Material(Shader.Find("Sprites/Outline"));
			renderer.sprite = ResourceLoader.TaskSprite;
			SetOutline(false);
			collider = gameObject.AddComponent<BoxCollider2D>();
			collider.isTrigger = false;
			collider.size /= 4f;
			collider.edgeRadius = 0.1f;
		}

		public float CanUse(GameData.PlayerInfo PC, out bool CanUse) {
			PlayerControl Player = PC.Object;
			Vector2 truePosition = Player.GetTruePosition();
			CanUse = !PC.IsDead && Player.CanMove;
			float Distance = float.MaxValue;

			if (CanUse) {
				Distance = Vector2.Distance(truePosition, transform.position);
				CanUse &= (Distance <= UsableDistance);
			}

			return Distance;
		}

		public void Use(PlayerControl LocalPlayer) {
			BetterAirShip.Logger.LogInfo("Hey !");
		}

		public void SetOutline(bool On) {
			if (renderer) {
				renderer.material.SetFloat("_Outline", (float) (On ? 1 : 0f));
				renderer.material.SetColor("_OutlineColor", Color.white);
				renderer.material.SetColor("_AddColor", On ? Color.white : Color.clear);
			}
		}

		public static void CreateThisTask(Vector3 Position) {
			GameObject CallPlateform = new GameObject("Call Plateform");
			CallPlateform.transform.position = Position;
			CallPlateform.transform.localScale = new Vector3(1f, 1f, 2f);
			CallPlateform.layer = 12;
			CallPlateform.SetActive(true);

			Tasks CallPlateformTasks = CallPlateform.AddComponent<Tasks>();
			CallPlateformTasks.Id = 1;
			AllCustomPlateform.Add(CallPlateform);
		}

		public static void ClosestTasks(PlayerControl Player) {
			NearestTask = null;

			foreach (var CustomElectrical in AllCustomPlateform) {
				Tasks component = CustomElectrical.GetComponent<Tasks>();
				component.SetOutline(false);
				if (component != null && ((!Player.Data.IsDead && (!AmongUsClient.Instance || !AmongUsClient.Instance.IsGameOver) && Player.CanMove) || !Player.inVent)) {
					float Distance = component.CanUse(Player.Data, out bool CanUse);

					if (CanUse && Distance < component.UsableDistance) {
						NearestTask = component;
						component.SetOutline(true);
					}
				}
			}
		}
	}

	[HarmonyPatch(typeof(UseButtonManager), nameof(UseButtonManager.DoClick))]
	public static class UseButtonOnClickPatch {
		public static bool Prefix(UseButtonManager __instance) {
			if (__instance.isActiveAndEnabled && PlayerControl.LocalPlayer && Tasks.NearestTask != null) {
				Tasks.NearestTask.Use(PlayerControl.LocalPlayer);
				return false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(UseButtonManager), nameof(UseButtonManager.SetTarget))]
	public static class UseButtonSetTargetPatch {
		public static bool Prefix(UseButtonManager __instance) {
			Tasks.ClosestTasks(PlayerControl.LocalPlayer);
			if (__instance.isActiveAndEnabled && PlayerControl.LocalPlayer && Tasks.NearestTask != null) {
				__instance.UseButton.color = new Color(1f, 1f, 1f, 1f);
				return false;
			}

			return true;
		}
	}
}
