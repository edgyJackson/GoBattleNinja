using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoBattleLeagueTeamBuilder.Models;
using GoBattleLeagueTeamBuilder.Models.Interfaces;
using System.Net.Http;

namespace GoBattleLeagueTeamBuilder.Controllers {
	public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IPokedexRepository _pokedex;
        private readonly IAdminUtilities _AdminUtilities;
        private readonly IGameMasterRepository _IGameMasterRepository;
        private readonly IHttpClientFactory _IHttpClientFactory;
        public HomeController(ILogger<HomeController> logger, IPokedexRepository pokedex, IAdminUtilities AdminUtilities, IGameMasterRepository IGameMasterRepository, IHttpClientFactory IHttpClientFactory)
        {
            _logger = logger;
            _pokedex = pokedex;
            _AdminUtilities = AdminUtilities;
            _IGameMasterRepository = IGameMasterRepository;
            _IHttpClientFactory = IHttpClientFactory;
    }

		public async Task<IActionResult> IndexAsync() {
			/*_AdminUtilities.GeneratePokedexSeedFileWithBaseStats();*/
			await _IGameMasterRepository.UpdatePokemonData(_IHttpClientFactory);
			/*await _AdminUtilities.GreatLeagueAsync();*/
			return View();
			}

		[HttpPost]
        public IActionResult GetPokedex()
        {        
            return Json(_pokedex.GetAllPokemon());
        }


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
