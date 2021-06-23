using System.Reflection;
using Harion.Reactor;
using Harion.Utility.Utils;
using UnityEngine;

namespace BetterAirShip {
    public static class AssetsLoader {
        private static readonly Assembly MyAssembly = Assembly.GetExecutingAssembly();
        public static Sprite VaultSprite;
        public static Sprite CokpitSprite;
        public static Sprite TaskSprite;
        public static Sprite MedicalSprite;
        public static AnimationClip VaultAnim;
        public static AnimationClip CokpitAnim;
        public static AnimationClip MedicalAnim;
        public static GameObject CallPlateform;

        public static void LoadAssets() {
            var resourceSteam = MyAssembly.GetManifestResourceStream("BetterAirShip.Resources.Airship");
            var assetBundle = AssetBundle.LoadFromMemory(resourceSteam.ReadFully());
            VaultSprite = assetBundle.LoadAsset<Sprite>("Vault").DontDestroy();
            CokpitSprite = assetBundle.LoadAsset<Sprite>("Cokpit").DontDestroy();
            MedicalSprite = assetBundle.LoadAsset<Sprite>("Medical").DontDestroy();
            TaskSprite = assetBundle.LoadAsset<Sprite>("task-shields").DontDestroy();

            VaultAnim = assetBundle.LoadAsset<AnimationClip>("Vault.anim").DontDestroy();
            CokpitAnim = assetBundle.LoadAsset<AnimationClip>("Cokpit.anim").DontDestroy();
            MedicalAnim = assetBundle.LoadAsset<AnimationClip>("Medical.anim").DontDestroy();
            CallPlateform = assetBundle.LoadAsset<GameObject>("call.prefab").DontDestroy();
        }
    }
}
