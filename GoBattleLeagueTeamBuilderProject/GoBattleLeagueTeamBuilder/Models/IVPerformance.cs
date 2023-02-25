using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models
{
    public class IVPerformance
    {
        public IV iVS { get; set; }
        public double level { get; set; }
        public stat stats { get; set; }
        public int cP { get; set; }
        public double statProduct { get; set; }

    }

    public class IV
    {
        public double atkIV { get; set; }
        public double defIV { get; set; }
        public double staIV { get; set; }
    }

    public class stat
    {
        public double atkStat { get; set; }
        public double defStat { get; set; }
        public double staStat { get; set; }
    }
}
