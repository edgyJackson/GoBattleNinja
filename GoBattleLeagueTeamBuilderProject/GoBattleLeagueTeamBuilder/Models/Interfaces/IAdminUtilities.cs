using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models.Interfaces
{
    public interface IAdminUtilities
    {
        Task littleLeagueAsync();
        Task GreatLeagueAsync();
        Task UltraLeagueAsync();
        Task UltraLeagueClassicAsync();
        void generatePokedexSeedFileWithBaseStats();
        void generatePokedexSeedFileWithBestIVs();
    }
}
