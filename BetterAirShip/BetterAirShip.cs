using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using BetterAirShip.Utility;
using Essentials.Options;
using HarmonyLib;
using Reactor;

namespace BetterAirShip {
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]

    public class BetterAirShip : BasePlugin {
        public const string Id = "fr.evanhardel.betterairship";
        public static ManualLogSource Logger;

        public Harmony Harmony { get; } = new Harmony(Id);

        public static CustomOptionHeader AirshipHeader = CustomOptionHeader.AddHeader("[0015CCFF]BetterAirShip Options :[]");
        public static CustomStringOption TypeSpawn = CustomStringOption.AddString("Type Of Spawn", new string[] { "Normal", "Fixed", "Synchronized" });
        public static CustomToggleOption NewSpawn = CustomToggleOption.AddToggle("Add new spawn", false);
        public static CustomToggleOption MeetingRespawn = CustomToggleOption.AddToggle("Choose spawn after meeting", true);
        public static CustomNumberOption minTimeDoor = CustomNumberOption.AddNumber("Min time for door swipe", 0.4f, 0f, 1f, 0.05f);

        public override void Load() {
            Logger = Log;
            Harmony.PatchAll();
            ResourceLoader.LoadAssets();
            RegisterInIl2CppAttribute.Register();
            GameOptionsFormat();
        }

        private void GameOptionsFormat() {
            CustomOption.ShamelessPlug = false;

            AirshipHeader.HudStringFormat = (option, name, value) => $"\n{name}";
            minTimeDoor.ValueStringFormat = (_, value) => $"{value}s";
        }
    }
}
