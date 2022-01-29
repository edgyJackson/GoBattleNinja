using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models.Interfaces
{
    public interface IAdminUtilities
    {
        Task LittleLeagueAsync();
        Task GreatLeagueAsync();
        Task GreatLeagueClassicAsync();
        Task UltraLeagueAsync();
        Task UltraLeagueClassicAsync();
        void GeneratePokedexSeedFileWithBaseStats();
        void GeneratePokedexSeedFileWithBestIVs();
    }
}
