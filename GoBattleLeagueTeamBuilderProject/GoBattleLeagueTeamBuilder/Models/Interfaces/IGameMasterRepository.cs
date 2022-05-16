using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models.Interfaces
{
    public interface IGameMasterRepository
    {
        void GetGameMasterFileWithNewtonSoft();
        Task HTTPClientGetJsonFromUrl(IHttpClientFactory iHttpClientFactory, string url);
    }
}