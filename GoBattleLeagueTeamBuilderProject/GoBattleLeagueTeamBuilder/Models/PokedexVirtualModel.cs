using System.Collections.Generic;

namespace GoBattleLeagueTeamBuilder.Models {
	public class PokedexVirtualModel {
		public PokemonDataLists PokemonDataLists { get; set; }
		public List<Pokedex> ListPokedex { get; set; }
		public List<string> PokemonForms { get; set; }

	}
}
