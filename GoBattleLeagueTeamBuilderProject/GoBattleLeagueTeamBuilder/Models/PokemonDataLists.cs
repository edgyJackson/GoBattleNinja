using System.Collections.Generic;

namespace GoBattleLeagueTeamBuilder.Models
{


    public class PokemonDataLists
    {
        public string templateId { get; set; } //

        public List<Combatleague> ListCombatLeague { get; set; } //30837 
        public List<Combattype> ListCombatType { get; set; } //36100 
        public List<Combatmove> ListCombatMove { get; set; } //36888 
        public List<Formsettings> ListFormSettings { get; set; } //40614 
        public List<Typeeffective> ListTypeEffective { get; set; } //55807 
        public List<Pokemonfamily> ListPokemonFamily { get; set; } //79513 
        public List<Pokemonsettings> ListPokemonSettings { get; set; } //79524 
        public List<Weatheraffinities> ListWeatherAffinities { get; set; } //233696 

    }
}
