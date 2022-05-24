﻿using GoBattleLeagueTeamBuilder.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoBattleLeagueTeamBuilder.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.IO;

namespace GoBattleLeagueTeamBuilder.Models.Repositories {
  public class GameMasterRepository:IGameMasterRepository {
    private readonly ISendHTTPWebRequest ISendHTTPWebRequest_;
    private IHttpClientFactory IHttpClientFactory_;
    List<PokemonUtility> theGMF;
    public GameMasterRepository(ISendHTTPWebRequest ISendHTTPWebRequest,IAdminUtilities AdminUtilities,IHttpClientFactory IHttpClientFactory) {
      ISendHTTPWebRequest_=ISendHTTPWebRequest;
      IHttpClientFactory_=IHttpClientFactory;
    }
    /*public void GetGameMasterFileWithNewtonSoft()
    {
        var gameMasterFileJsonString = ISendHTTPWebRequest_.GetJsonFromUrl("https://raw.githubusercontent.com/PokeMiners/game_masters/master/latest/latest.json");

        JArray geo = JArray.Parse(gameMasterFileJsonString);
            
    }*/

    public async Task HTTPClientGetJsonFromUrl(IHttpClientFactory HttpClientFactory) {
      string text = "";
      string gameFileTimeStamp;
      string errorString;
      var request = new HttpRequestMessage(HttpMethod.Get,"https://raw.githubusercontent.com/PokeMiners/game_masters/master/latest/timestamp.txt");
      var client = HttpClientFactory.CreateClient();

      //If we have the timestamp file then read the timestamp from it
			if(File.Exists("timestamp.txt")) {
        text=File.ReadAllText("timestamp.txt");
      }
      //if we didn't get a timestamp the file didn't exist, get the file and save it to timestamp.txt
      using HttpResponseMessage response = await client.SendAsync(request);
      if(!response.IsSuccessStatusCode) {
        errorString=$"There was an error getting our time stamp: {response.ReasonPhrase}";
        Console.WriteLine(errorString);
        return;
      }
      gameFileTimeStamp = await response.Content.ReadAsStringAsync();
      //Pass the filepath and filename to the StreamWriter Constructor
      if(text=="") {
        StreamWriter sw = new("timestamp.txt");
        sw.Write(gameFileTimeStamp);
        //Close the file
        sw.Close();
        text=File.ReadAllText("timestamp.txt");
      }

      //check if the current timestamp is older than the new timestamp.
		  if(long.Parse(gameFileTimeStamp)>long.Parse(text)) {
        StreamWriter sw = new("timestamp.txt");
        sw.Write(gameFileTimeStamp);
        sw.Close();
        await getGameMasterFile(client);
      }
			if(!File.Exists(@"./Data/gameMasterFile.json")) {
        await getGameMasterFile(client);
			}
    }
    public async Task getGameMasterFile(HttpClient client) {
      var request=new HttpRequestMessage(HttpMethod.Get,"https://raw.githubusercontent.com/PokeMiners/game_masters/master/latest/latest.json");
      using(HttpResponseMessage response = await client.SendAsync(request)) {
        if(response.IsSuccessStatusCode) {
          string gameMasterFile = await response.Content.ReadAsStringAsync();
          //Pass the filepath and filename to the StreamWriter Constructor
          StreamWriter sw = new(@"./Data/gameMasterFile.json");
          sw.WriteLine(gameMasterFile);
          sw.Close();
        }
      }
    }

    public async Task UpdatePokemonData(IHttpClientFactory HttpClientFactory) {
      await HTTPClientGetJsonFromUrl(HttpClientFactory);
      using var fs = new FileStream(@"./Data/gameMasterFile.json", FileMode.Open,FileAccess.Read);
      List<PokemonUtility>  theGMF =await JsonSerializer.DeserializeAsync<List<PokemonUtility>>(fs,new JsonSerializerOptions { PropertyNameCaseInsensitive=true });
      PokemonDataLists pokemonDataLists = await GeneratePokemonDataAsync(theGMF);
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
