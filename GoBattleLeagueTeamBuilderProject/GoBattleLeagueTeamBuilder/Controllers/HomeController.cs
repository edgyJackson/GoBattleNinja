using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoBattleLeagueTeamBuilder.Models;
using GoBattleLeagueTeamBuilder.Models.Interfaces;
using System.Net.Http;
using System.Linq;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace GoBattleLeagueTeamBuilder.Controllers {
	public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IPokedexRepository _pokedex;
    private readonly IAdminUtilities _AdminUtilities;
    private readonly IGameMasterRepository _IGameMasterRepository;
    private readonly IHttpClientFactory _IHttpClientFactory;
    private readonly GoBattleLeagueTeamBuilderDBContext _goBattleLeagueTeamBuilderDBContext;
     
    public HomeController(ILogger<HomeController> logger, IPokedexRepository pokedex, IAdminUtilities AdminUtilities, IGameMasterRepository IGameMasterRepository,  IHttpClientFactory IHttpClientFactory, GoBattleLeagueTeamBuilderDBContext goBattleLeagueTeamBuilderDBContext) {
      _logger = logger;
      _pokedex = pokedex;
      _AdminUtilities = AdminUtilities;
      _IGameMasterRepository = IGameMasterRepository;
      _IHttpClientFactory = IHttpClientFactory;
      _goBattleLeagueTeamBuilderDBContext = goBattleLeagueTeamBuilderDBContext;
    }

		public IActionResult /*async Task<IActionResult>*/ IndexAsync() {
			//await _IGameMasterRepository.UpdateThePokedexAsync(_IHttpClientFactory);
			_AdminUtilities.GeneratePokedexSeedFileWithBestIVs();
			return View();
		}

    public  IActionResult GetPokedex()
    {
      //get pokemon data
      if(!System.IO.File.Exists(@"./Data/pokemonDataListsJson.json")) {
        return View(); 
      }
      PokemonDataLists pokemonDataLists = new();
      PokedexVirtualModel pokedexVirtualModel = new PokedexVirtualModel();
      using var fs = new FileStream(@"./Data/pokemonDataListsJson.json",FileMode.Open,FileAccess.Read);
      pokemonDataLists =  JsonSerializer.Deserialize<PokemonDataLists>(fs,new JsonSerializerOptions {PropertyNameCaseInsensitive=true});
      pokedexVirtualModel.PokemonDataLists=pokemonDataLists;
      pokedexVirtualModel.ListPokedex=_pokedex.GetAllPokemon();
			pokedexVirtualModel.PokemonForms=pokedexVirtualModel.ListPokedex.Select(x => x.Form).Distinct().ToList();
			return Json(pokedexVirtualModel);
    }

		/*[HttpPost]
      public async Task<IActionResult> GetPokedexAsync()
      {
        //get pokemon data
        if(!System.IO.File.Exists(@"./Data/pokemonDataListsJson.json")) {
          return View(); 
        }
        PokemonDataLists pokemonDataLists = new();
        PokedexVirtualModel pokedexVirtualModel = new PokedexVirtualModel();
        using var fs = new FileStream(@"./Data/pokemonDataListsJson.json",FileMode.Open,FileAccess.Read);
        pokemonDataLists = await JsonSerializer.DeserializeAsync<PokemonDataLists>(fs,new JsonSerializerOptions {PropertyNameCaseInsensitive=true});
        //pokedexVirtualModel.PokemonForms=pokemonDataLists.ListFormSettings.Where(x=>x.forms!=null).SelectMany(f => f.forms.Select(form => form.form)).ToList();
        pokedexVirtualModel.PokemonDataLists=pokemonDataLists;
        pokedexVirtualModel.ListPokedex=await _pokedex.GetAllPokemonAsync();
        //need to use this for am inute to get the forms with names
        *//*pokedexVirtualModel.PokemonForms= pokedexVirtualModel.ListPokedex.Select(x=> x.Form).Distinct().ToList();*//*
			  //Pass the filepath and filename to the StreamWriter Constructor
			  *//*StreamWriter sw=new(@"./Data/pokemonFormsWithNames.txt");
			  for(int i = 0;i<pokedexVirtualModel.PokemonForms.Count;i++) {
				  sw.WriteLine(pokedexVirtualModel.PokemonForms[i]);
			  }
			  sw.Close();*/
			/*StreamWriter nsw = new(@"./Data/pokemonNames.txt");
			for(int i = 0;i<pokedexVirtualModel.ListPokedex.Count;i++) {
				nsw.WriteLine(pokedexVirtualModel.ListPokedex[i].Name);
			}   
			nsw.Close(); *//*
			return Json(pokedexVirtualModel);
      }*/


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
