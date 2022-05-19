using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GoBattleLeagueTeamBuilder.Models
{
    public class PokemonSortingUtilities
    {
        //Data and variables needed for processing pokemon information
        Regex speciesNameRegEx = new Regex(@"/^\S*/");
        Regex speciesFormRegEx = new Regex(@"/\(.+?\)/");
        Dictionary<string, string> pokemonForms = new Dictionary<string, string> { { "Galarian", "31" }, { "Defense", "13" }, {"Shadow", "00" }, { "Alolan", "61" }, { "Snowy", "14" }, { "Rainy", "13" }, { "Sunny", "12" }, { "Attack", "12" }, { "Speed", "14" }, { "Plant", "11" }, { "Sandy", "12" }, { "Trash", "13" }, { "Overcast", "11" }, { "Sunshine", "12" }, { "West", "11" }, { "East", "12" }, { "Regular", "11" }, { "Heat", "12" }, { "Wash", "13" }, { "Fan", "15" }, { "Frost", "14" }, { "Mow", "15" }, { "Origin", "12" }, { "Altered", "11" }, { "Land", "11" }, { "Sky", "12" }, { "Standard", "11" }, { "Zen", "12" }, { "Spring", "11" }, { "Summer", "12" }, { "Autumn", "13" }, { "Winter", "14" }, { "Incarnate", "11" }, { "Therian", "12" }, { "White", "12" }, { "Black", "13" }, { "Ordinary", "11" }, { "Resolute", "12" }, { "Aria", "11" }, { "Pirouette", "12" }, { "Douse", "12" }, { "Burn", "14" }, { "Shock", "13" }, { "Chill", "15" }, { "Armored", "50" }, { "Hero", "11" }, { "Unbound", "11" }, { "Average", "11" }, { "Large", "12" }, { "Small", "13" }, { "Super", "14" }, { "Male", "00"}, { "Female", "01"}, { "Libre", "16" }, { "5th Anniversary", "12" }, { "Flying", "11" }, { "Kariyushi", "13" }, { "Rock Star", "14" }, { "Pop Star", "15" }, { "Jr","00" }};
         Dictionary<string, string> dict = new Dictionary<string, string> { { "Little League", "500" }, { "Great League", "1500" }, { "Great League Remix", "1500" }, { "Kanto Cup", "1500" }, { "Sinnoh Cup", "1500" }, { "Holiday Cup", "1500" }, { "Ultra League", "2500" }, { "Ultra League Remix", "2500" }, { "Ultra League Premier", "2500" }, { "Ultra League Premier Classic", "2500" }, { "Master League", "10000" }, { "Master League Classic", "10000" }, { "Master League Premier", "10000" }, { "Little League Premier Classic", "500" }, { "Great League Premier Classic", "1500" }, { "Master League Premier Classic", "10000" } };
        /*var TypeColors = {"" };*/
        //async causing json not to load properly, set to false
    }
}
