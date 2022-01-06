using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoBattleLeagueTeamBuilder.Models;
using GoBattleLeagueTeamBuilder.Models.Interfaces;

namespace GoBattleLeagueTeamBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPokedexRepository _pokedex;
        private readonly IAdminUtilities _AdminUtilities;

        public HomeController(ILogger<HomeController> logger, IPokedexRepository pokedex, IAdminUtilities AdminUtilities)
        {
            _logger = logger;
            _pokedex = pokedex;
            _AdminUtilities = AdminUtilities;
        }

        public IActionResult Index()
        {          
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
