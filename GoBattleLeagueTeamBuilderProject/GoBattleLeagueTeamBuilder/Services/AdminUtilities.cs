using GoBattleLeagueTeamBuilder.Models;
using GoBattleLeagueTeamBuilder.Models.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Services
{
    public class AdminUtilities : IAdminUtilities
    {
        private readonly IPVP_IVsAPIRepository _pvpIVAPI;
        private readonly IPokedexRepository _pokedex;
        private readonly ISendHTTPWebRequest _SendHttpRequest;
        private readonly IRepository<Pokedex> _repo;
        public AdminUtilities(IPVP_IVsAPIRepository pvpIVAPIkedex, IPokedexRepository pokedex, ISendHTTPWebRequest SendHttpRequest, IRepository<Pokedex> repo)
        {
            _pvpIVAPI = pvpIVAPIkedex;
            _pokedex = pokedex;
            _SendHttpRequest = SendHttpRequest;
            _repo = repo;
        }
        public async Task littleLeagueAsync()
        {
            for (int i = 1; i <_pokedex.GetCount(); i++)
            {
                var pokedexEntry = _pokedex.GetPokedexEntryByID(i);
                var TheIVPerformance = _pvpIVAPI.getBestIV(500, (int)pokedexEntry.BaseAtk, (int)pokedexEntry.BaseDef, (int)pokedexEntry.BaseSta, 51);

                pokedexEntry.LlatkIv = TheIVPerformance.iVS.atkIV;
                pokedexEntry.LldefIv = TheIVPerformance.iVS.defIV;
                pokedexEntry.LlstaIv = TheIVPerformance.iVS.staIV;
                pokedexEntry.Lllevel = TheIVPerformance.level;
                pokedexEntry.LlcP = TheIVPerformance.cP;
                pokedexEntry.LlatkStat = TheIVPerformance.stats.atkStat;
                pokedexEntry.LldefStat = TheIVPerformance.stats.defStat;
                pokedexEntry.LlstaStat = TheIVPerformance.stats.staStat;
                pokedexEntry.LlstatProduct = TheIVPerformance.statProduct;
                await _repo.AddOrUpdateAsync(pokedexEntry);
            }          
        }

        public async Task GreatLeagueAsync()
        {
            for (int i = 1; i < _pokedex.GetCount(); i++)
            {
                var pokedexEntry = _pokedex.GetPokedexEntryByID(i);
                var TheIVPerformance = _pvpIVAPI.getBestIV(1500, (int)pokedexEntry.BaseAtk, (int)pokedexEntry.BaseDef, (int)pokedexEntry.BaseSta, 51);

                pokedexEntry.GlatkIv = TheIVPerformance.iVS.atkIV;
                pokedexEntry.GldefIv = TheIVPerformance.iVS.defIV;
                pokedexEntry.GlstaIv = TheIVPerformance.iVS.staIV;
                pokedexEntry.Gllevel = TheIVPerformance.level;
                pokedexEntry.GlcP = TheIVPerformance.cP;
                pokedexEntry.GlatkStat = TheIVPerformance.stats.atkStat;
                pokedexEntry.GldefStat = TheIVPerformance.stats.defStat;
                pokedexEntry.GlstaStat = TheIVPerformance.stats.staStat;
                pokedexEntry.GlstatProduct = TheIVPerformance.statProduct;
                await _repo.AddOrUpdateAsync(pokedexEntry);
            }
        }

        public async Task UltraLeagueAsync()
        {
            for (int i = 1; i < _pokedex.GetCount(); i++)
            {
                var pokedexEntry = _pokedex.GetPokedexEntryByID(i);
                var TheIVPerformance = _pvpIVAPI.getBestIV(2500, (int)pokedexEntry.BaseAtk, (int)pokedexEntry.BaseDef, (int)pokedexEntry.BaseSta, 51);

                pokedexEntry.UlatkIv = TheIVPerformance.iVS.atkIV;
                pokedexEntry.UldefIv = TheIVPerformance.iVS.defIV;
                pokedexEntry.UlstaIv = TheIVPerformance.iVS.staIV;
                pokedexEntry.Ullevel = TheIVPerformance.level;
                pokedexEntry.UlcP = TheIVPerformance.cP;
                pokedexEntry.UlatkStat = TheIVPerformance.stats.atkStat;
                pokedexEntry.UldefStat = TheIVPerformance.stats.defStat;
                pokedexEntry.UlstaStat = TheIVPerformance.stats.staStat;
                pokedexEntry.UlstatProduct = TheIVPerformance.statProduct;
                await _repo.AddOrUpdateAsync(pokedexEntry);
            }
        }

        public async Task UltraLeagueClassicAsync()
        {
            for (int i = 1; i < _pokedex.GetCount(); i++)
            {
                var pokedexEntry = _pokedex.GetPokedexEntryByID(i);
                var TheIVPerformance = _pvpIVAPI.getBestIV(2500, (int)pokedexEntry.BaseAtk, (int)pokedexEntry.BaseDef, (int)pokedexEntry.BaseSta, 41);

                pokedexEntry.UlclassicatkIv = TheIVPerformance.iVS.atkIV;
                pokedexEntry.UlclassicdefIv = TheIVPerformance.iVS.defIV;
                pokedexEntry.UlclassicstaIv = TheIVPerformance.iVS.staIV;
                pokedexEntry.Ulclassiclevel = TheIVPerformance.level;
                pokedexEntry.UlclassiccP = TheIVPerformance.cP;
                pokedexEntry.UlclassicatkStat = TheIVPerformance.stats.atkStat;
                pokedexEntry.UlclassicdefStat = TheIVPerformance.stats.defStat;
                pokedexEntry.UlclassicstaStat = TheIVPerformance.stats.staStat;
                pokedexEntry.UlclassicstatProduct = TheIVPerformance.statProduct;
                await _repo.AddOrUpdateAsync(pokedexEntry);
            }
        }

        public void generatePokedexSeedFileWithBaseStats()
        {
            //get all pokemon base stats from pokmemon go api
            string url = "https://pogoapi.net/api/v1/pokemon_stats.json";
            string PokemonStatsJsonString = _SendHttpRequest.getJsonFromUrl(url);
            JArray geo = JArray.Parse(PokemonStatsJsonString);

            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new("output.txt");

                //Write a line of text
                for (int i = 0; i < geo.Count; i++)
                {
                    sw.WriteLine("(" + geo[i]["pokemon_id"] + ",'" + geo[i]["pokemon_name"] + "','" + geo[i]["form"] + "'," + geo[i]["base_attack"] + "," + geo[i]["base_defense"] + "," + geo[i]["base_stamina"] + ")");
                }
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        public void generatePokedexSeedFileWithBestIVs()
        {
            //get all pokemon from db
          
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new("output2.txt");

                //Write a line of text
                foreach (var item in _repo.GetAll())
                {
                    sw.WriteLine("(" + item.PokemonId + ",'" + item.Name + "','" + item.Form + "'," + item.BaseAtk + "," + item.BaseDef + "," + item.BaseSta + "," + item.LlatkIv + "," + item.LldefIv + "," + item.LlstaIv + "," + item.Lllevel + "," + item.LlcP + "," + item.LlatkStat + "," + item.LldefStat + "," + item.LlstaStat + "," + item.LlstatProduct + "," + item.GlatkIv + "," + item.GldefIv + "," + item.GlstaIv + "," + item.Gllevel + "," + item.GlcP + "," + item.GlatkStat + "," + item.GldefStat + "," + item.GlstaStat + "," + item.GlstatProduct + "," + item.UlatkIv + "," + item.UldefIv + "," + item.UlstaIv + "," + item.Ullevel + "," + item.UlcP + "," + item.UlatkStat + "," + item.UldefStat + "," + item.UlstaStat + "," + item.UlstatProduct + "," + item.UlclassicatkIv + "," + item.UlclassicdefIv + "," + item.UlclassicstaIv + "," + item.Ulclassiclevel + "," + item.UlclassiccP + "," + item.UlclassicatkStat + "," + item.UlclassicdefStat + "," + item.UlclassicstaStat + "," + item.UlclassicstatProduct + ")");
                }
                
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
