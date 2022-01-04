$(document).ready(function() 
{

    //Data and variables needed for processing pokemon information
    var speciesNameRegEx = new RegExp("/^\S*/"); 
    var speciesFormRegEx = new RegExp("/\(.+?\)/");
    var pokemonForms = { "Galarian": 31, "Defense": 13, "Shadow": '00', "Alolan": 61, "Snowy": 14, "Rainy": 13, "Sunny": 12, "Attack": 12, "Speed": 14, "Plant": 11, "Sandy": 12, "Trash": 13, "Overcast": 11, "Sunshine": 12, "West": 11, "East": 12, "Regular": 11, "Heat": 12, "Wash": 13, "Fan": 15, "Frost": 14, "Mow": 15, "Origin": 12, "Altered": 11, "Land": 11, "Sky": 12, "Standard": 11, "Zen": 12, "Spring": 11, "Summer": 12, "Autumn": 13, "Winter": 14, "Incarnate": 11, "Therian": 12, "White": 12, "Black": 13, "Ordinary": 11, "Resolute": 12, "Aria": 11, "Pirouette": 12, "Douse": 12, "Burn": 14, "Shock": 13, "Chill": 15, "Armored": 50, "Hero": 11, "Unbound": 11, "Average": 11, "Large": 12, "Small": 13, "Super": 14, "Male": '00', "Female": '01', "Libre": 16, "5th Anniversary": 12, "Flying": 11,"Kariyushi":13,"Rock Star":14,"Pop Star":15, "Jr":'00'};
    var dict = { "Little League": "500", "Great League": "1500", "Great League Remix": "1500", "Kanto": "1500", "Holiday": "1500", "Ultra League": "2500", "Ultra League Remix": "2500", "Master League": "10000", "Master League Classic": "10000" };
    /*var TypeColors = {""};*/
    //async causing json not to load properly: set to false
    var pokedex = $.ajax({
        type: "POST",
        url: "/Home/GetPokedex",
        async: false,
        dataType: 'json',
    }).responseJSON;



    //functions used while processing and displaying pokemon information
    $(".menu-area a ").click(function() {

        let jsonString = "https://raw.githubusercontent.com/pvpoke/pvpoke/master/src/data/rankings/" + this.id + "/overall/rankings-" + dict[this.textContent] + ".json";

        document.getElementById("headerText").innerText = this.textContent;

        $.getJSON(jsonString, function(data) {
           
            $(".MainBox").empty();

            var speciesNameString;
            var pokemonName;
            var ThePokemonsIndex;
            var pokemonForm;
            var shadowOrPurifiedOrXL ="";
                               
            for (i = 0;i < 25; i++) {      

                speciesNameString = data[i].speciesName;
                pokemonForm = "00";
                shadowOrPurifiedOrXL = "";
                
                if (!speciesNameString.includes("(")) {
                    pokemonName = speciesNameString;
                } else {
                    var match = /(^[^\(]+) \((.+)\)/.exec(speciesNameString);
                    pokemonName = match[1];
                    pokemonForm = pokemonForms[match[2]];

                if (match[2] == "Shadow") {
                    shadowOrPurifiedOrXL = "<img src=\"../images/Pokemon/ic_shadow.png\" class=\"ShadowOrPurified\"/>";
                    }
                }

                if (data[i].moveset.includes("RETURN")) {
                    shadowOrPurifiedOrXL = "<img src=\"../images/Pokemon/ic_purified.png\" class=\"ShadowOrPurified\"/>";
                }

               /* if (!data[i].speciesId.includes("_xs")) {
                    shadowOrPurifiedOrXL = shadowOrPurifiedOrXL + "<div class=\"XLDiv\">XL</div>";
                }*/
                var pokedexMon = pokedex.find(obj => obj.name == pokemonName);

                var ThePokemonsID = String(pokedexMon.pokemonId).padStart(3, '0');

                $(".MainBox").append("<a href=\"#\"><div class='MainBoxUL' id=\"" + ThePokemonsID + "\"><img id=\"PokemonImage\" src=\"../images/Pokemon/pokemon_icon_" + ThePokemonsID + "_" + pokemonForm + ".png\"/\">" + shadowOrPurifiedOrXL + "</div></a>");
            }
             
        });

    });

});


