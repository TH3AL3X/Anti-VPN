using Rocket.Core.Logging;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Darkness
{
    class PlayerComponent : UnturnedPlayerComponent
    {
        public void Checker(UnturnedPlayer player)
        {
            using (WebClient web = new WebClient())
            {
                string jsonReader = @"{""status"":""success"",""result"":""0"",""queryIP"":""" + player.IP + @""",""queryFlags"":""m"",""queryOFlags"":null,""queryFormat"":""json"",""contact"":""themachinehack@gmail.com""}";

                string jsonReader1 = @"{""status"":""success"",""result"":""1"",""queryIP"":""" + player.IP + @""",""queryFlags"":""m"",""queryOFlags"":null,""queryFormat"":""json"",""contact"":""themachinehack@gmail.com""}";

                var result = web.DownloadString(AntiVPN.Instance.Configuration.Instance.URL + player.IP);

                if (result.Equals(jsonReader1))
                {
                    Logger.Log($"Kicked player {player.CharacterName}");
                    player.Kick(AntiVPN.Instance.Translate("kickmessage"));
                    web.Dispose();
                }
                else if (result.Equals(jsonReader))
                {
                    Logger.Log(AntiVPN.Instance.Translate("nokickmessage"));
                    web.Dispose();
                }

            }
        }
    }
}
