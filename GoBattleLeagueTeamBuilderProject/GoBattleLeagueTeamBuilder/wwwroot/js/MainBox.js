$(document).ready(function() 
{

    //Data and variables needed for processing pokemon information
    var speciesNameRegEx = new RegExp("/^\S*/"); 
    var speciesFormRegEx = new RegExp("/\(.+?\)/");
    var pokemonForms = { "Galarian": 31, "Defense": 13, "Shadow": '00', "Alolan": 61, "Snowy": 14, "Rainy": 13, "Sunny": 12, "Attack": 12, "Speed": 14, "Plant": 11, "Sandy": 12, "Trash": 13, "Overcast": 11, "Sunshine": 12, "West": 11, "East": 12, "Regular": 11, "Heat": 12, "Wash": 13, "Fan": 15, "Frost": 14, "Mow": 15, "Origin": 12, "Altered": 11, "Land": 11, "Sky": 12, "Standard": 11, "Zen": 12, "Spring": 11, "Summer": 12, "Autumn": 13, "Winter": 14, "Incarnate": 11, "Therian": 12, "White": 12, "Black": 13, "Ordinary": 11, "Resolute": 12, "Aria": 11, "Pirouette": 12, "Douse": 12, "Burn": 14, "Shock": 13, "Chill": 15, "Armored": 50, "Hero": 11, "Unbound": 11, "Average": 11, "Large": 12, "Small": 13, "Super": 14, "Male": '00', "Female": '01', "Libre": 16, "5th Anniversary": 12, "Flying": 11,"Kariyushi":13,"Rock Star":14,"Pop Star":15, "Jr":'00'};
    var dict = { "Little League": "500", "Great League": "1500", "Great League Remix": "1500", "Kanto": "1500", "Holiday": "1500", "Ultra League": "2500", "Ultra League Remix": "2500", "Ultra League Premier": "2500", "Ultra League Premier Classic": "2500", "Master League": "10000", "Master League Classic": "10000", "Master League Premier": "10000", "Little League Premier Classic": "500", "Great League Premier Classic": "1500", "Master League Premier Classic": "10000" };
    /*var TypeColors = {""};*/
    //async causing json not to load properly: set to false
    var pokedex =$.ajax({
        type: "POST",
        url: "/Home/GetPokedex",
        async: false,
        dataType: 'json',
    }).responseJSON;



    //functions used while processing and displaying pokemon information
    $(".menu-area a ").click(function() {

        let jsonString = "https://raw.githubusercontent.com/pvpoke/pvpoke/master/src/data/rankings/" + this.id + "/overall/rankings-" + dict[this.textContent] + ".json";

        document.getElementById("headerText").innerText = this.textContent;
        var LeagueSelected = this.textContent;
        $.getJSON(jsonString, function(data) {
           
            $(".MainBox").empty();

            
            for (i = 0;i < 950; i++) {      
                var speciesNameString = "";
                var pokemonName = "";
                var pokemonForm = "";
                var shadowOrPurifiedOrXL = "";
                var match = "";
                var ThePokemonsID = "";

                //get the pokemons name string from pvpoke json set pokemon form and shadowpurifiedxl to default
                speciesNameString = data[i].speciesName;
                pokemonForm = "00";
                shadowOrPurifiedOrXL = "";              

                //check if the pokemon has a form if some parse out the form and name
                if (!speciesNameString.includes("(")) {
                    pokemonName = speciesNameString;
                } else {
                    match = /(^[^\(]+) \((.+)\)/.exec(speciesNameString);
                    pokemonName = match[1];
                    pokemonForm = pokemonForms[match[2]];
                }

                //special check for nidorans and darmanitans they are annoying and then figure out which pokemon it is and create an object 
                if (pokemonName.includes('Nidoran')) {
                    pokemonName = 'Nidoran';
                    if (data[i].speciesId.includes('female')) {
                        var pokedexMon = pokedex.filter(obj => obj.pokemonId == 29 && obj.name == pokemonName);
                    } else {
                        var pokedexMon = pokedex.filter(obj => obj.pokemonId == 32 && obj.name == pokemonName);
                    }
                } else if (data[i].speciesId.includes('galarian_standard')) {                   
                    var pokedexMon = pokedex.filter(obj => obj.form == 'Galarian_standard' && obj.name == pokemonName);
                } else if (data[i].speciesId.includes('galarian_zen')) {
                    var pokedexMon = pokedex.filter(obj => obj.form == 'Galarian_zen' && obj.name == pokemonName);
                } else if (match == "" || match[2] == "Shadow" || match[2] == "Jr") {
                    var pokedexMon = pokedex.filter(obj => obj.name == pokemonName && obj.form == "Normal");
                } else {
                    var pokedexMon = pokedex.filter(obj => obj.name == pokemonName && obj.form == match[2]);
                }

                //get the pokemons id for the mainbox string concatenation
                ThePokemonsID = String(pokedexMon[0].pokemonId).padStart(3, '0');

                //check for shadow
                if (match[2] == "Shadow") {
                    shadowOrPurifiedOrXL = "<img src=\"../images/Pokemon/ic_shadow.png\" class=\"ShadowOrPurified\"/>";
                    }
                //check for purified
                if (data[i].moveset.includes("RETURN")) {
                    shadowOrPurifiedOrXL = "<img src=\"../images/Pokemon/ic_purified.png\" class=\"ShadowOrPurified\"/>";
                }
                //check for XL
                if (!data[i].speciesId.includes("_xs")) {

                    if (LeagueSelected == "Little League" || LeagueSelected == "Little League Premier Classic" ) {
                        if (pokedexMon[0].lllevel > 41) {
                            shadowOrPurifiedOrXL = shadowOrPurifiedOrXL + "<div class=\"XLDiv\">XL</div>";
                        }
                    }
                    else if (LeagueSelected == "Great League" || LeagueSelected == "Great League Remix" || LeagueSelected == "Kanto" || LeagueSelected == "Holiday" || LeagueSelected == "Great League Premier Classic") {
                        if (pokedexMon[0].gllevel > 41) {
                            shadowOrPurifiedOrXL = shadowOrPurifiedOrXL + "<div class=\"XLDiv\">XL</div>";
                        }
                    }
                    else if (LeagueSelected == "Ultra League" || LeagueSelected == "Ultra League Remix" || LeagueSelected == "Ultra League Premier") {
                        if (pokedexMon[0].ullevel > 41) {
                            shadowOrPurifiedOrXL = shadowOrPurifiedOrXL + "<div class=\"XLDiv\">XL</div>";
                        }
                    }
                    else if (LeagueSelected == "Master League" || LeagueSelected == "Master League Premier") {
                            shadowOrPurifiedOrXL = shadowOrPurifiedOrXL + "<div class=\"XLDiv\">XL</div>";
                    }
                }
                
                //print the html to the dom
                $(".MainBox").append("<a href=\"#\"><div class='MainBoxUL' id=\"" + ThePokemonsID + "\"><img id=\"PokemonImage\" src=\"../images/Pokemon/pokemon_icon_" + ThePokemonsID + "_" + pokemonForm + "_shiny.png\"/\">" + shadowOrPurifiedOrXL + "</div></a>");
            }          
        });
    });
});


