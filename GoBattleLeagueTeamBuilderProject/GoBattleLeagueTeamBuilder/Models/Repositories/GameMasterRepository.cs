using GoBattleLeagueTeamBuilder.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoBattleLeagueTeamBuilder.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GoBattleLeagueTeamBuilder.Models.Repositories
{
  public class GameMasterRepository : IGameMasterRepository
  {
    private readonly ISendHTTPWebRequest ISendHTTPWebRequest_;
    private IHttpClientFactory IHttpClientFactory_;
    List<PokemonUtility> theGMF;
    public GameMasterRepository(ISendHTTPWebRequest ISendHTTPWebRequest, IAdminUtilities AdminUtilities, IHttpClientFactory IHttpClientFactory)
    {
      ISendHTTPWebRequest_ = ISendHTTPWebRequest;
      IHttpClientFactory_ = IHttpClientFactory;
    }
    /*public void GetGameMasterFileWithNewtonSoft()
    {
        var gameMasterFileJsonString = ISendHTTPWebRequest_.GetJsonFromUrl("https://raw.githubusercontent.com/PokeMiners/game_masters/master/latest/latest.json");

        JArray geo = JArray.Parse(gameMasterFileJsonString);
            
    }*/

    public async Task<PokemonDataLists> HTTPClientGetJsonFromUrl(IHttpClientFactory HttpClientFactory)
    {
      string errorString;
      var request = new HttpRequestMessage(HttpMethod.Get, "https://raw.githubusercontent.com/PokeMiners/game_masters/master/latest/latest.json");
      var client = HttpClientFactory.CreateClient();
      using (HttpResponseMessage response = await client.SendAsync(request))
      {
        if (response.IsSuccessStatusCode)
        {
          var stream = await response.Content.ReadAsStreamAsync();

          theGMF = await JsonSerializer.DeserializeAsync<List<PokemonUtility>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
          errorString = "";
          PokemonDataLists pokemonDataLists = await GeneratePokemonDataAsync(theGMF);
          return await GeneratePokemonDataAsync(theGMF);
          }
        else
        {
          errorString = $"There was an error getting our forecast: {response.ReasonPhrase}";
          Console.WriteLine(errorString);
          return null;
        }
      }
    }

    public async Task<PokemonDataLists> GeneratePokemonDataAsync(List<PokemonUtility> theGMF) {
      PokemonDataLists pokemonDataLists = new PokemonDataLists() {
        ListCombatLeague=new List<Combatleague>(),
        ListCombatType=new List<Combattype>(),
        ListCombatMove=new List<Combatmove>(),
        ListFormSettings=new List<Formsettings>(),
        ListTypeEffective=new List<Typeeffective>(),
        ListPokemonFamily=new List<Pokemonfamily>(),
        ListPokemonSettings=new List<Pokemonsettings>(),
        ListWeatherAffinities=new List<Weatheraffinities>()
      };

      for(int i = 0;i<theGMF.Count;i++) {
        if(theGMF[i].data!.combatLeague!=null) {
            Combatleague combatLeague = theGMF[i].data!.combatLeague!;
            pokemonDataLists.ListCombatLeague.Add(combatLeague);
            continue;
        }
        if(theGMF[i].data!.combatType!=null) {
            pokemonDataLists.ListCombatType.Add(theGMF[i].data!.combatType!);
            continue;
        }
        if(theGMF[i].data!.combatMove!=null) {
            pokemonDataLists.ListCombatMove.Add(theGMF[i].data!.combatMove!);
            continue;
        }
        if(theGMF[i].data!.formSettings!=null) {
            pokemonDataLists.ListFormSettings.Add(theGMF[i].data!.formSettings!);
            continue;
        }
        if(theGMF[i].data!.typeEffective!=null) {
            pokemonDataLists.ListTypeEffective.Add(theGMF[i].data!.typeEffective!);
            continue;
        }
        if(theGMF[i].data!.pokemonFamily!=null) {
            pokemonDataLists.ListPokemonFamily.Add(theGMF[i].data!.pokemonFamily!);
            continue;
        }
        if(theGMF[i].data!.pokemonSettings!=null) {
            pokemonDataLists.ListPokemonSettings.Add(theGMF[i].data!.pokemonSettings!);
            continue;
        }
        if(theGMF[i].data!.weatherAffinities!=null) {
            pokemonDataLists.ListWeatherAffinities.Add(theGMF[i].data!.weatherAffinities!);
            continue;
        }
      }
      return await Task.FromResult(pokemonDataLists);
    }
  }
}