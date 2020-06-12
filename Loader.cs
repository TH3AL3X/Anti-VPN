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
            try
            {
                ThreadPool.QueueUserWorkItem(yes => Checker(player));
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void Checker(UnturnedPlayer player)
        {
            using (WebClient web = new WebClient())
            {
                string jsonReader = @"{""status"":""success"",""result"":""0"",""queryIP"":""" + player.IP + @""",""queryFlags"":""m"",""queryOFlags"":null,""queryFormat"":""json"",""contact"":""themachinehack@gmail.com""}";

                string jsonReader1 = @"{""status"":""success"",""result"":""1"",""queryIP"":""" + player.IP + @""",""queryFlags"":""m"",""queryOFlags"":null,""queryFormat"":""json"",""contact"":""themachinehack@gmail.com""}";

                var result = web.DownloadString(AntiVPN.Instance.Configuration.Instance.URL + player.IP);

                if (result.Equals(jsonReader1))
                {
                    Logger.Log($"Kicked player {player.CharacterName}");
                    player.Kick(Translate("kickmessage"));
                    web.Dispose();
                }
                else if (result.Equals(jsonReader))
                {
                    Logger.Log(Translate("nokickmessage"));
                    web.Dispose();
                }
            }
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