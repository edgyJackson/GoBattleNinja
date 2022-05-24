using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models.Interfaces
{
  public interface IGameMasterRepository
  {
    Task<PokemonDataLists> GeneratePokemonDataAsync(List<PokemonUtility> theGMF);

    /*    void GetGameMasterFileWithNewtonSoft();*/
    Task HTTPClientGetJsonFromUrl(IHttpClientFactory iHttpClientFactory);
    Task UpdatePokemonData(IHttpClientFactory HttpClientFactory);
  }
}