﻿using System.Reflection;
using Reactor.Extensions;
using UnityEngine;

namespace BetterAirShip {
    public static class ResourceLoader {
        private static readonly Assembly myAsembly = Assembly.GetExecutingAssembly();
        public static Sprite VaultSprite;
        public static Sprite CokpitSprite;
        public static Sprite TaskSprite;
        public static AnimationClip VaultAnim;
        public static AnimationClip CokpitAnim;
        public static GameObject CallPlateform;

        public static void LoadAssets() {
            var resourceSteam = myAsembly.GetManifestResourceStream("BetterAirShip.Resource.airship");
            var assetBundle = AssetBundle.LoadFromMemory(resourceSteam.ReadFully());
            VaultSprite = assetBundle.LoadAsset<Sprite>("Vault").DontDestroy();
            CokpitSprite = assetBundle.LoadAsset<Sprite>("Cokpit").DontDestroy();
            TaskSprite = assetBundle.LoadAsset<Sprite>("task-shields").DontDestroy();

            VaultAnim = assetBundle.LoadAsset<AnimationClip>("Vault.anim").DontDestroy();
            CokpitAnim = assetBundle.LoadAsset<AnimationClip>("Cokpit.anim").DontDestroy();
            CallPlateform = assetBundle.LoadAsset<GameObject>("call.prefab").DontDestroy();
        }
    }
}