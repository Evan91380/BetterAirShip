using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using Essentials.Options;
using HarmonyLib;
using Reactor;

namespace AirShipSpawn{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class AirshipSpawn : BasePlugin {
        public const string Id = "fr.evan.airshipspawn";
        public static ManualLogSource Logger;

        public Harmony Harmony { get; } = new Harmony(Id);

        public static CustomOptionHeader AirshipHeader = CustomOptionHeader.AddHeader("\n[2EADFFFF]BetterAirShip Options :[]", false);
        public static CustomStringOption TypeSpawn = CustomStringOption.AddString("Type Of Spawn", new string[] { "Normal", "Fixed", "Synchronized" });
        public static CustomToggleOption NewSpawn = CustomToggleOption.AddToggle("Add new spawn", false);
        public static CustomToggleOption MeetingRespawn = CustomToggleOption.AddToggle("Choose spawn after meeting", true);
        public static CustomNumberOption minTimeDoor = CustomNumberOption.AddNumber("Min time for door swipe", 0.4f, 0f, 1f, 0.05f);


        public override void Load() {
        
            Harmony.PatchAll();
            Logger = Log;
            Logger.LogInfo("AirshipSpawn Mods is ready !");
            AirShipSpawn.Utility.ResourceLoader.LoadAssets();
            CustomOption.ShamelessPlug = false;

            minTimeDoor.HudStringFormat = (_, name, value) => $"{name}: {value}s";

        }
    }
}
