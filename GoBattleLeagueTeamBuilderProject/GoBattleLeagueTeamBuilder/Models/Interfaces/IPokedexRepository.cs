using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models.Interfaces
{
    public interface IPokedexRepository
    {
        List<string> GetAllPokemonNames();
        List<Pokedex> GetAllPokemon();
        int GetCount();
        Pokedex GetPokedexEntryByID(int Id);

        Pokedex GetPokemonWithHighestLLStatProduct();
    }
}
