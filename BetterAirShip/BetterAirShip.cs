using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
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

        public static CustomOptionHeader AirshipHeader = CustomOptionHeader.AddHeader("<color=#0015CCFF>BetterAirShip General :</color>");
        public static CustomStringOption TypeSpawn = CustomStringOption.AddString("Spawn type", new string[] { "Normal", "Fixed", "Random Synchronized" });
        public static CustomToggleOption NewSpawn = CustomToggleOption.AddToggle("Add new spawn", false);
        public static CustomToggleOption MeetingRespawn = CustomToggleOption.AddToggle("Meeting Respawn", false);
        public static CustomToggleOption CallPlateform = CustomToggleOption.AddToggle("Call Plateform Button", false);
        public static CustomToggleOption Teleportation = CustomToggleOption.AddToggle("Meeting/Security teleportation", false);
        public static CustomNumberOption minTimeDoor = CustomNumberOption.AddNumber("Min time for door swipe", 0.4f, 0f, 10f, 0.05f);

        public static CustomOptionHeader AirshipTasksHeader = CustomOptionHeader.AddHeader("<color=#0015CCFF>BetterAirShip Tasks :</color>");
        public static CustomStringOption MoveAdmin = CustomToggleOption.AddString("Move Admin", new string[] { "Don't Move", "Move To Right of Cockpit", "Move To Main Hall" });
        public static CustomStringOption MoveElectrical = CustomToggleOption.AddString("Move Electical GapRoom", new string[] { "Don't Move", "Move To Vault", "Move To Electrical" });
        public static CustomToggleOption CargoGas = CustomToggleOption.AddToggle("Move Fuel", false);
        public static CustomToggleOption VitalsMedbay = CustomToggleOption.AddToggle("Move Vitals", false);
        public static CustomToggleOption Divert = CustomToggleOption.AddToggle("Move Divert", false);


        public override void Load() {
            GameOptionsFormat();
            Logger = Log;
            Harmony.PatchAll();
            ResourceLoader.LoadAssets();
            RegisterInIl2CppAttribute.Register();
        }

        private void GameOptionsFormat()
        {
            CustomOption.ShamelessPlug = false;

            AirshipHeader.HudStringFormat = (option, name, value) => $"\n{name}";
            AirshipTasksHeader.HudStringFormat = (option, name, value) => $"\n{name}";

            minTimeDoor.ValueStringFormat = (_, value) => $"{value}s";
        }
    }
}
