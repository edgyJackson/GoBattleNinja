using GoBattleLeagueTeamBuilder.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models.Repositories
{
    public class PokedexRepository : Repository<Pokedex>, IPokedexRepository
    {
        public PokedexRepository(GoBattleLeagueTeamBuilderDBContext ctx) : base(ctx)
        {

        }

        public virtual List<string> GetAllPokemonNames() { 
            return _dbSet.Select(x => x.Name).ToList();
        }

        public virtual List<Pokedex> GetAllPokemon()
        {
            return _dbSet.OrderBy(x => x.Id).ToList();
        }
        public virtual async Task<List<Pokedex>> GetAllPokemonAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual int GetCount()
        {
            return _dbSet.Count();
        }

        public virtual Pokedex GetPokedexEntryByID(int Id)
        {
            return _dbSet.Where(x => x.Id == Id).FirstOrDefault();
        }

        public virtual Pokedex GetPokemonWithHighestLLStatProduct()
        {
            return _dbSet.OrderByDescending(x => x.LlstatProduct).FirstOrDefault();
        }

    }
}
