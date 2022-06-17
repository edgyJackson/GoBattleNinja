using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models.Interfaces
{
    public interface IPokedexRepository
    {
        List<string> GetAllPokemonNames();
        List<Pokedex> GetAllPokemon();
        Task<List<Pokedex>> GetAllPokemonAsync();
        int GetCount();
        Pokedex GetPokedexEntryByID(int Id);
        Pokedex GetPokemonWithHighestLLStatProduct();
    }
}
