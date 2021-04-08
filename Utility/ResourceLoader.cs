using System.IO;
using System.Reflection;
using Reactor.Extensions;
using Reactor.Unstrip;
using UnityEngine;

namespace AirShipSpawn.Utility
{
    public static class ResourceLoader
    {
        private static readonly Assembly myAsembly = Assembly.GetExecutingAssembly();
        public static Sprite VaultSprite;
        public static Sprite CokpitSprite;
        public static AnimationClip VaultAnim;
        public static AnimationClip CokpitAnim;

        public static void LoadAssets()
        {
            var resourceSteam = myAsembly.GetManifestResourceStream("AirShipSpawnMod.Resource.airship");
            var assetBundle = AssetBundle.LoadFromMemory(resourceSteam.ReadFully());
            VaultSprite = assetBundle.LoadAsset<Sprite>("Vault").DontDestroy();
            CokpitSprite = assetBundle.LoadAsset<Sprite>("Cokpit").DontDestroy();

            VaultAnim = assetBundle.LoadAsset<AnimationClip>("Vault.anim").DontDestroy();
            CokpitAnim = assetBundle.LoadAsset<AnimationClip>("Cokpit.anim").DontDestroy();
        }
    }
}
