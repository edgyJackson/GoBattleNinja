using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models
{
    public class IVPerformance
    {
        public IV iVS { get; set; }
        public float level { get; set; }
        public stat stats { get; set; }
        public int cP { get; set; }
        public float statProduct { get; set; }

    }

    public class IV
    {
        public float atkIV { get; set; }
        public float defIV { get; set; }
        public float staIV { get; set; }
    }

    public class stat
    {
        public float atkStat { get; set; }
        public float defStat { get; set; }
        public float staStat { get; set; }
    }
}
