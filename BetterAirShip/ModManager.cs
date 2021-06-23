using Harion.ModsManagers;
using Harion.ModsManagers.Configuration;
using Harion.ModsManagers.Mods;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BetterAirShip {
    public class ModManager : ModRegistry, IModManager, IModManagerLink, IModManagerUpdater {

        public string DisplayName => "Better AirShip";

        public string Version => typeof(BetterAirShip).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        public string SmallDescription => "Improves the Airship map.";

        public string Description => "This mod adds modifications to the Airship map, which changes the place tasks, and removes some frustrations, The best, the doors of the toilets are syncronized.";

        public string Credit => "Evan & Hardel";

        public Dictionary<string, Sprite> ModsLinks => new Dictionary<string, Sprite>() {
            { "https://discord.gg/AP9axbXXNC", ModsSocial.GithubSprite }
        };

        public string GithubRepositoryName => "BetterAirShip";

        public string GithubAuthorName => "Evan91380";

        public GithubVisibility GithubRepositoryVisibility => GithubVisibility.Public;

        public string GithubAccessToken => "";
    }
}