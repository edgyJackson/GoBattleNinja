using GoBattleLeagueTeamBuilder.Models.Interfaces;
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
    private readonly GoBattleLeagueTeamBuilderDBContext _goBattleLeagueTeamBuilderDBContext;
    private readonly IAdminUtilities _AdminUtilities;
     
    public GameMasterRepository(GoBattleLeagueTeamBuilderDBContext goBattleLeagueTeamBuilderDBContext,IAdminUtilities AdminUtilities) {
      _goBattleLeagueTeamBuilderDBContext = goBattleLeagueTeamBuilderDBContext;
      _AdminUtilities = AdminUtilities;
    }
    /*public void GetGameMasterFileWithNewtonSoft()
    {
        var gameMasterFileJsonString = ISendHTTPWebRequest_.GetJsonFromUrl("https://raw.githubusercontent.com/PokeMiners/game_masters/master/latest/latest.json");
        JArray geo = JArray.Parse(gameMasterFileJsonString); 
    }*/
    public async Task UpdateThePokedexAsync(IHttpClientFactory _IHttpClientFactory)
    {
      PokemonDataLists pokemonDataLists=new();
      if(!await UpdatePokemonData(_IHttpClientFactory)) { 
        return; 
      }
      using var fs=new FileStream(@"./Data/pokemonDataListsJson.json",FileMode.Open,FileAccess.Read);
      pokemonDataLists=await JsonSerializer.DeserializeAsync<PokemonDataLists>(fs,new JsonSerializerOptions { PropertyNameCaseInsensitive=true });
      List<Pokedex>listPokedexes=new();
      for(int i = 0;i<pokemonDataLists.ListPokemonSettings.Count;i++) {
        listPokedexes.Add(new Pokedex {
          PokemonId=int.Parse(pokemonDataLists.ListPokemonSettings[i].templateId.Substring(1,4)),
          Name=pokemonDataLists.ListPokemonSettings[i].pokemonSettings.pokemonId,
          Form=(pokemonDataLists.ListPokemonSettings[i].pokemonSettings.form!=null) ? pokemonDataLists.ListPokemonSettings[i].pokemonSettings.form.Replace(pokemonDataLists.ListPokemonSettings[i].pokemonSettings.pokemonId+"_","") : "NO_FORM",
          BaseAtk=pokemonDataLists.ListPokemonSettings[i].pokemonSettings.stats.baseAttack,
          BaseDef=pokemonDataLists.ListPokemonSettings[i].pokemonSettings.stats.baseDefense,
          BaseSta=pokemonDataLists.ListPokemonSettings[i].pokemonSettings.stats.baseStamina
        });
      }
      //Add new pokemon to pokedex and update log files
      using(StreamWriter sw = File.AppendText("GameMasterFileUpdateLogs.txt")) {
        //Update the pokedex
        for(int i = 0;i<listPokedexes.Count;i++) {
          if(_goBattleLeagueTeamBuilderDBContext.Pokedexes.Any(c => c.Name==listPokedexes[i].Name&&c.Form==listPokedexes[i].Form)) {
            continue;
          }
          await _goBattleLeagueTeamBuilderDBContext.Pokedexes.AddAsync(listPokedexes[i]);
          sw.WriteLine("\r\n"+listPokedexes[i].PokemonId + " - " + listPokedexes[i].Name + " " + listPokedexes[i].Form + " " + listPokedexes[i].BaseAtk + " " + listPokedexes[i].BaseDef + " " + listPokedexes[i].BaseSta );
        }
      }
      await _AdminUtilities.GetPVPIVSForAllLeagues();
      /*await _AdminUtilities.GreatLeagueAsync();*/
    }

    public async Task<bool> UpdatePokemonData(IHttpClientFactory HttpClientFactory) {
      return await HTTPClientGetJsonFromUrlAsync(HttpClientFactory);
    }

    public async Task<bool> HTTPClientGetJsonFromUrlAsync(IHttpClientFactory HttpClientFactory) {
      bool gameMasterFileWasUpdated=false;
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
        return false;
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
		  if(!File.Exists(@"./Data/gameMasterFile.json") || long.Parse(gameFileTimeStamp)>long.Parse(text)) {
        StreamWriter sw = new("timestamp.txt");
        sw.Write(gameFileTimeStamp);
        sw.Close();
        await getGameMasterFile(client);
        gameMasterFileWasUpdated = true;
      }
      //log any updates to the game master file
      if(!File.Exists("GameMasterFileUpdateLogs.txt")) 
      {
        StreamWriter sw = new("GameMasterFileUpdateLogs.txt");
        sw.Write("Game Master File Log Created " + DateTime.Now);
        sw.Close();
			}
			else if(gameMasterFileWasUpdated){
        StreamWriter sw = File.AppendText("GameMasterFileUpdateLogs.txt");
        sw.Write("\r\nGame Master File Updated " + DateTime.Now);
        sw.Close();
			}
      return gameMasterFileWasUpdated;
    }

    public async Task getGameMasterFile(HttpClient client) {
      var request=new HttpRequestMessage(HttpMethod.Get,"https://raw.githubusercontent.com/PokeMiners/game_masters/master/latest/latest.json");
      using(HttpResponseMessage response = await client.SendAsync(request)) {
        if(response.IsSuccessStatusCode) {
          var gameMasterFile = await response.Content.ReadAsStreamAsync();
          List<PokemonUtility> theGMF = await JsonSerializer.DeserializeAsync<List<PokemonUtility>>(gameMasterFile,new JsonSerializerOptions { PropertyNameCaseInsensitive=true });
          PokemonDataLists pokemonDataLists = await GeneratePokemonDataAsync(theGMF);
          string pokemonDataListsJsonString = JsonSerializer.Serialize(pokemonDataLists);
          //Pass the filepath and filename to the StreamWriter Constructor
          StreamWriter sw = new(@"./Data/pokemonDataListsJson.json");
          sw.WriteLine(pokemonDataListsJsonString);
          sw.Close(); 
          //Pass the filepath and filename to the StreamWriter Constructor
          sw = new(@"./Data/gameMasterFile.json");
          sw.WriteLine(gameMasterFile);
          sw.Close();
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
        ListPokemonSettings=new List<PokemonsettingsData>(),
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
          pokemonDataLists.ListPokemonSettings.Add(new PokemonsettingsData { templateId = theGMF[i].data!.templateId, pokemonSettings = theGMF[i].data!.pokemonSettings });
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
