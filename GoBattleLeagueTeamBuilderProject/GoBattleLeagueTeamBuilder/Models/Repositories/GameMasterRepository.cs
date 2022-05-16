using GoBattleLeagueTeamBuilder.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoBattleLeagueTeamBuilder.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace GoBattleLeagueTeamBuilder.Models.Repositories
{
    public class GameMasterRepository : IGameMasterRepository
    {
        private readonly ISendHTTPWebRequest _ISendHTTPWebRequest;

        public GameMasterRepository(ISendHTTPWebRequest ISendHTTPWebRequest, IAdminUtilities AdminUtilities)
        {
            _ISendHTTPWebRequest = ISendHTTPWebRequest;
        }
        public void GetGameMasterFileWithNewtonSoft()
        {
            var gameMasterFileJsonString = _ISendHTTPWebRequest.GetJsonFromUrl("https://raw.githubusercontent.com/PokeMiners/game_masters/master/latest/latest.json");

            JArray geo = JArray.Parse(gameMasterFileJsonString);
            
        }

        public async Task HTTPClientGetJsonFromUrl(IHttpClientFactory iHttpClientFactory, string url)
        {
            PokemonUtility pokemonUtility;
            String errorString;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = iHttpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                /*gameMasterFileModel = await JsonConvert.DeserializeObject<List<GameMasterFileModel>>(response.Content.ReadAsStringAsync());*/
                pokemonUtility = await response.Content.ReadFromJsonAsync<PokemonUtility>();
            }
            else{
                errorString = $"There was an error getting our GameMasterFile: {response.ReasonPhrase}"; 
            }
            

        }
    }
}
