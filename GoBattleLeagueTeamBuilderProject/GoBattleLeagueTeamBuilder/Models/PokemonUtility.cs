using System.Collections.Generic;

namespace GoBattleLeagueTeamBuilder.Models
{
    public class PokemonUtility
    {
        public string templateId { get; set; }

        public Data2 data { get; set; }

    }
    public class Data2
    {
        public string templateId { get; set; }

        public Pokemonsettings pokemonSettings { get; set; }

        public Combatmove combatMove { get; set; }

        public Pokemonfamily pokemonFamily { get; set; }

        public Combattype combatType { get; set; }

        public Combatleague combatLeague { get; set; }

        public Formsettings formSettings { get; set; }

        public Typeeffective typeEffective { get; set; }

        public Weatheraffinities weatherAffinities { get; set; }

    }
}
