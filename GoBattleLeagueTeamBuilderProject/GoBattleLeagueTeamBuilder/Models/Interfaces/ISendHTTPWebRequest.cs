using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models.Interfaces
{
    public interface ISendHTTPWebRequest
    {
        string getJsonFromUrl(string url);
    }
}
