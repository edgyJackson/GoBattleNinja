using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models.Interfaces
{
    public interface IPVP_IVsAPIRepository
    {
        void PrintBestIV(int league, int baseAtk, int baseDef, int baseSta, float xLStatus);
       

        IVPerformance GetBestIV(int league, int baseAtk, int baseDef, int baseSta, float xLStatus);
       
    }
}
