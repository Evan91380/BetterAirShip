using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using Harion.CustomOptions;
using Harion.Reactor;
using Harion;
using HarmonyLib;

namespace BetterAirShip {
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(HarionPlugin.Id)]

    public class BetterAirShip : BasePlugin {
        public const string Id = "fr.evanhardel.betterairship";
        public static ManualLogSource Logger;

        public Harmony Harmony { get; } = new Harmony(Id);

        public static CustomOptionHolder AirshipHolder = CustomOption.AddHolder("<color=#0015CCFF>BetterAirShip General :</color>");
        public static CustomStringOption TypeSpawn = CustomOption.AddString("Spawn type", AirshipHolder, new string[] { "Normal", "Fixed", "Random Synchronized" });
        public static CustomToggleOption NewSpawn = CustomOption.AddToggle("Add new spawn", false, AirshipHolder);
        public static CustomToggleOption MeetingRespawn = CustomOption.AddToggle("Meeting Respawn", false, AirshipHolder);
        public static CustomToggleOption CallPlateform = CustomOption.AddToggle("Call Plateform Button", false, AirshipHolder);
        public static CustomToggleOption Teleportation = CustomOption.AddToggle("Meeting/Security teleportation", false, AirshipHolder);
        public static CustomNumberOption minTimeDoor = CustomOption.AddNumber("Min time for door swipe", 0.4f, 0f, 10f, 0.05f, AirshipHolder);
        public static CustomNumberOption CrashCourseTime = CustomOption.AddNumber("Time for crash course sabotage", 90f, 30f, 100f, 5f, AirshipHolder);

        public static CustomOptionHolder AirshipTasksHolder = CustomOption.AddHolder("<color=#0015CCFF>BetterAirShip Tasks :</color>");
        public static CustomStringOption MoveAdmin = CustomOption.AddString("Move Admin", AirshipTasksHolder, new string[] { "Don't Move", "Move To Right of Cockpit", "Move To Main Hall" });
        public static CustomStringOption MoveElectrical = CustomOption.AddString("Move Electical GapRoom", AirshipTasksHolder, new string[] { "Don't Move", "Move To Vault", "Move To Electrical" });
        public static CustomToggleOption CargoGas = CustomOption.AddToggle("Move Fuel", false, AirshipTasksHolder);
        public static CustomToggleOption VitalsMedbay = CustomOption.AddToggle("Move Vitals", false, AirshipTasksHolder);
        public static CustomToggleOption Divert = CustomOption.AddToggle("Move Divert", false, AirshipTasksHolder);


        public override void Load() {
            Logger = Log;
            Harmony.PatchAll();
            AssetsLoader.LoadAssets();
            RegisterInIl2CppAttribute.Register();
            GameOptionsFormat();
        }

        private void GameOptionsFormat() {
            CustomOption.ShamelessPlug = false;

            AirshipHolder.HudStringFormat = (option, name, value) => $"\n{name}";
            AirshipTasksHolder.HudStringFormat = (option, name, value) => $"\n{name}";

            minTimeDoor.ValueStringFormat = (_, value) => $"{value}s";
            CrashCourseTime.ValueStringFormat = (_, value) => $"{value}s";
        }
    }
}
