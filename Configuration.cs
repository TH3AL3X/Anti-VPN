using Rocket.API;
using System.Collections.Generic;
using System.Reflection;

namespace Darkness
{
    public class Configuration : IRocketPluginConfiguration
    {
        public string URL;
        public void LoadDefaults()
        {
            URL = "https://plugins.darknesscommunity.club/vpn-api.php?ip=";
        }
    }
}
