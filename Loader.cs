using System.Collections.Generic;
using System.Collections;
using SDG.Unturned;
using Steamworks;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using UnityEngine;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Rocket.Core.Plugins;
using Rocket.Core.Utils;
using Rocket.API;
using Rocket.Core;
using Rocket.Core.Extensions;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using Rocket.API.Collections;

namespace Darkness
{
    public class AntiVPN : RocketPlugin<Configuration>
    {
        public static AntiVPN Instance;

        protected override void Load()
        {
            Instance = this;
            U.Events.OnPlayerConnected += OnPlayerConnected;
        }
 
        private void OnPlayerConnected(UnturnedPlayer player)
        {
            player.Player.GetComponent<PlayerComponent>().Checker(player);
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"kickmessage", "[Anti-VPN] VPN Detected"},
                    {"nokickmessage", "[Anti-VPN] No VPN found"}
                };
            }
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            Logger.Log("** Unloading **", ConsoleColor.Red);
        }
    }
}