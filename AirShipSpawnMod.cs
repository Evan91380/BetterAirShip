using System;
using System.Linq;
using System.Net;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using Essentials.Options;
using HarmonyLib;
using Reactor;
using UnhollowerBaseLib;

namespace AirShipSpawn{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class AirshipSpawn : BasePlugin {
        public const string Id = "fr.evan.airshipspawn";
        public static ManualLogSource Logger;

        public Harmony Harmony { get; } = new Harmony(Id);

        public static CustomStringOption TypeSpawn = CustomStringOption.AddString("Type Of Spawn", new string[] { "Normal", "Fixed", "Random" });
        public static CustomToggleOption MeetingRespawn = CustomToggleOption.AddToggle("Choose spawn after meeting", true);

        public override void Load() {
        
            Harmony.PatchAll();
            Logger = Log;
            Logger.LogInfo("AirshipSpawn Mods is ready !");

        }
    }
}
