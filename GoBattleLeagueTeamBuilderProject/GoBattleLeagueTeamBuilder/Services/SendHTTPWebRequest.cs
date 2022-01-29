using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GoBattleLeagueTeamBuilder.Models.Interfaces;

namespace GoBattleLeagueTeamBuilder.Services
{
    public class SendHTTPWebRequest : ISendHTTPWebRequest
    {
        public string GetJsonFromUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string jsonString = null;
            // TODO: You should handle exceptions here
            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }

            return jsonString;
        }
    }
}
