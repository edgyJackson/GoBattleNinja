//Data and variables needed for processing pokemon information
var speciesNameRegEx = new RegExp("/^\S*/"); 
var speciesFormRegEx = new RegExp("/\(.+?\)/");
var pokemonForms = { "PIKACHU_FLYING":99,"STEEL":11,"ROCK":11,"PSYCHIC":11,"POISON":11,"ICE":11,"GROUND":11,"GRASS":11,"GHOST":11,"FIRE":11,"FIGHTING":11,"FAIRY":11,"ELECTRIC":11,"DRAGON":11,"DARK":11,"BUG":11,"ORIGINAL_COLOR":11,"ULTRA":11,"DUSK_MANE":11,"DAWN_WINGS":11,"YELLOW":20,"VIOLET":11,"RED":18,"ORANGE":17,"BUSTED":11,"INDIGO":11,"BLUE":16,"GREEN":11,"DISGUISED":11,"HANGRY":11,"NOICE":11,"FULL_BELLY":11,"CROWNED_SWORD":11,"ICE_RIDER":11,"SINGLE_STRIKE":11,"RAPID_STRIKE":11,"ETERNAMAX":11,"CROWNED_SHIELD":11,"PHONY": 11,"ANTIQUE": 11,"LOW_KEY": 11,"AMPED": 11,"STAR": 11,"PHARAOH": 11,"NATURAL": 11,"MATRON": 11,"LA_REINE": 11,"KABUKI": 11,"HEART": 11,"DIAMOND": 11, "DEBUTANTE": 11,"DANDY":11,"SOLO": 11, "SCHOOL": 11,"MIDNIGHT": 11, "MIDDAY": 12,"DUSK":11,"CONFINED":12,"SENSU": 16,"PAU": 19, "POM_POM": 17,"BAILE":18,"ALOLA":61,"HISUIAN":72,"COSTUME_2020":11,"A":11,"2021":11,"VS_2019":11,"FLYING_5TH_ANNIV":11,"COPY_2019": 11, "FALL_2019": 11,"ADVENTURE_HAT_2020":11,"FLYING_OKINAWA":11,"2020": 11,"2022": 11,"S": 11,"SHADOW_RIDER":11,"WATER":21,"EAST_SEA": 12,"WEST_SEA":11,"WINTER_2020": '00',"RED_STRIPED": 11, "BLUE_STRIPED": 12,"GALARIAN_ZEN": 12,"GALARIAN_STANDARD":31,"NORMAL":'00',"GALARIAN": 31, "DEFENSE": 13, "SHADOW": '00', "ALOLAN": 61, "SNOWY": 14, "RAINY": 13, "SUNNY": 12, "ATTACK": 12, "SPEED": 14, "PLANT": 11, "SANDY": 12, "TRASH": 13, "OVERCAST": 11, "SUNSHINE": 12, "WEST": 11, "EAST": 12, "REGULAR": 11, "HEAT": 12, "WASH": 13, "FAN": 15, "FROST": 14, "MOW": 15, "ORIGIN": 12, "ALTERED": 11, "LAND": 11, "SKY": 12, "STANDARD": 11, "ZEN": 12, "SPRING": 11, "SUMMER": 12, "AUTUMN": 13, "WINTER": 14, "INCARNATE": 11, "THERIAN": 12, "WHITE": 12, "BLACK": 13, "ORDINARY": 11, "RESOLUTE": 12, "ARIA": 11, "PIROUETTE": 12, "DOUSE": 12, "BURN": 14, "SHOCK": 13, "CHILL": 15, "ARMORED": 50, "HERO": 11, "UNBOUND": 11, "AVERAGE": 11, "LARGE": 12, "SMALL": 13, "SUPER": 14, "MALE": '00', "FEMALE": '01', "LIBRE": 16, "5TH_ANNIVERSARY": 12, "FLYING": 11, "KARIYUSHI": 13, "ROCK_STAR": 14, "POP_STAR": 15, "JR": '00', "NO_FORM":'00'};
var dict = { "Fossil Cup": "1500","Go Battle League ML":"10000","CliffHanger ML":"10000","Championship Series ML":"10000","Mega ML":"10000","Go Battle League UL":"2500","CliffHanger UL":"2500","Championship Series UL":"2500","Mega UL":"2500","CliffHanger LL": "500","Championship Series LL":"500","Go Battle League LL":"500","Mega LL": "500","Mega GL":"1500","Retro Cup": "1500","Championship Series GL": "1500","Go Battle League GL": "1500","Forged Cup": "1500","Firefly Cup": "1500","Colony Cup": "1500","Alchemy Cup": "1500","River Cup":"1500", "Little League": "500", "Great League": "1500", "Great League Remix": "1500", "Kanto Cup": "1500", "Sinnoh Cup": "1500", "Holiday Cup": "1500", "Ultra League": "2500", "Ultra League Remix": "2500", "Ultra League Premier": "2500", "Ultra League Premier Classic": "2500", "Master League": "10000", "Master League Classic": "10000", "Master League Premier": "10000", "Little League Premier Classic": "500", "Great League Premier Classic": "1500", "Master League Premier Classic": "10000", "Love Cup": "1500" };
var WeirdNameList = ["HAKAMO_O", "HO_OH", "JANGMO_O", "KOMMO_O", "PORYGON_Z", "MIME_JR", "MR_MIME", "MR_RIME", "TAPU_BULU", "TAPU_FINI", "TAPU_KOKO", "TAPU_LELE", "TYPE_NULL","NIDORAN_MALE","NIDORAN_FEMALE"];
var NoHyphenList = ["MIME_JR", "MR_MIME", "MR_RIME", "TAPU_BULU", "TAPU_FINI", "TAPU_KOKO", "TAPU_LELE", "TYPE_NULL"];
/*var TypeColors = {""};*/
//async causing json not to load properly: set to false for debugging
/*var pokedex =$.ajax({
    type: "GET",
    url: "/Home/GetPokedex",
    async: true,
    dataType: 'json',
}).responseJSON;

$(document).ready(function () {
    SelectLeague();
});*/

$(document).ready(function () {
    $.ajax({
        type: "POST",
        dataType: "json",
        async: false,
        url: "Home/GetPokedex/",
        success: SelectLeague,
        error: errorOnAjax
    });
});

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

//search function for finding pokemon
function SearchFunction() {
    // Declare variables
    var input, filter, ul, li, a, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    ul = document.getElementById("myUL");
    li = ul.getElementsByTagName('li');

    // Loop through all list items, and hide those who don't match the search query
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        txtValue = a.id;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

 //functions used while processing and displaying pokemon information
function SelectLeague(pokedex) {
    $(".menu-area a ").click(function () {

        let jsonString = "https://raw.githubusercontent.com/pvpoke/pvpoke/master/src/data/rankings/" + this.id + "/overall/rankings-" + dict[this.textContent] + ".json";

        document.getElementById("headerText").innerText = this.textContent;
        var LeagueSelected = this.textContent;
        //for shiny button
        var shiny = "";
        $.getJSON(jsonString, function (data) {

            $(".MainBox ul").empty();

            var count = 1;
            for (i = 0; i < 950; i++) {
                var speciesID = "";
                var speciesNameString = "";
                var pokemonName = "";
                var pokemonForm = "";
                var shadowOrPurifiedOrXLBuddy = "";
                var match = "";
                var ThePokemonsID = "";
                var displayBestIVs = "";
                var countDiv = "<div class = 'countDiv'>" + count + "</div>";
                var PokemonNameDiv = "";
                var pokemonForm2 = "";
                var isWeirdName = false;

                //get the speciesId from pvpoke json and set it to uppercase; 
                speciesID = data[i].speciesId.toUpperCase();
                //check if speciesID could not be retireved
                if (speciesID == null || speciesID == "") {
                    console.log("Species Id was not found " + data[i].speciesId);
                }
                if (speciesID.endsWith("_XS")) {
                    speciesID = speciesID.replace("_XS", "");
                    LeagueSelected = "Great League Premier Classic";
                }
                //Check if shadow and fill shadowOrPurifiedOrXLBuddy if so, then remove the shadow from the speciesId
                if (speciesID.endsWith("_SHADOW")) {
                    shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/ic_shadow.png\" class=\"ShadowOrPurified\"/>";
                    speciesID=speciesID.replace("_SHADOW", "");
                }
                //check for purified
                if (data[i].moveset.includes("RETURN")) {
                  shadowOrPurifiedOrXLBuddy = "<img src=\"../images/Pokemon/ic_purified.png\" class=\"ShadowOrPurified\"/>";
                }
//----------------Parse out form from speciesId string----------------------------------------------------------------------------------------------------
                if (speciesID.includes("FLOETTE")) {
                    console.log("hey");
                }
                if (speciesID.includes("_")) {
                    var myArray = speciesID.split("_");
                    if (WeirdNameList.includes(myArray[0]+"_"+myArray[1])) {
                            pokemonName = myArray[0] + "_" + myArray[1];
                        if (myArray.length>2) {
                            for (var k = 2; k < myArray.length; k++) {
                                pokemonForm = myArray[k] + "_";
                            }
                            pokemonForm = pokemonForm.slice(0, -1);
                        }
                        else {
                            pokemonForm = "NO_FORM";
                        }
                    }
                    else {
                        pokemonName = myArray[0];
                        for (var j = 1; j < myArray.length; j++) {
                            pokemonForm = pokemonForm + myArray[j] + "_";
                        }
                        pokemonForm = pokemonForm.slice(0, -1);
                    }
                }
                else {
                    pokemonName = speciesID;
                    pokemonForm = "NO_FORM";
                }
                //special cases
                if (pokemonForm=="ALOLAN") {
                    pokemonForm = "ALOLA";
                }
                if (pokemonForm == "LIBRE") {
                    isWeirdName = true;
                    pokemonForm2 = "NORMAL";
                }
                if (pokemonForm == "5TH_ANNIVERSARY") {
                    isWeirdName = true;
                    pokemonForm2 = "FLYING_5TH_ANNIV";
                }
                if (speciesID=="PIKACHU_FLYING") {
                    isWeirdName = true;
                    pokemonForm2 = "NORMAL";
                    pokemonForm = speciesID;
                }
                if (speciesID == "MEWTWO_ARMORED") {
                    isWeirdName = true;
                    pokemonForm2 = "A";
                    pokemonForm = "ARMORED";
                }
                if (speciesID == "ORICORIO_POM_POM") {
                    isWeirdName = true;
                    pokemonForm2 = "POMPOM";
                }   
                /*if (speciesID == "NIDORAN_FEMALE" || "NIDORAN_MALE" || "NIDOKING_MALE") {
                isWeirdName = true;
                pokemonForm2 = "POMPOM";
                }*/
                speciesNameString = data[i].speciesName;
                if (shadowOrPurifiedOrXLBuddy.includes("ic_purified")) {
                    speciesNameString = speciesNameString + "(Purified)";
                }
                PokemonNameDiv = "<div class = 'PokemonNameDiv'>" + speciesNameString + "</div>";
                //get the pokemon from the pokedex
                if (isWeirdName) {
                    var pokedexMon = pokedex.listPokedex.filter(obj => obj.name == pokemonName && obj.form == pokemonForm2);
                } else {
                    var pokedexMon = pokedex.listPokedex.filter(obj => obj.name == pokemonName && obj.form == pokemonForm);
                }

                if(pokedexMon.length == 1) {
                    //get the pokemons id for the mainbox string concatenation
                    ThePokemonsID = pokedexMon[0].pokemonId.toString().padStart(3, "0");
                    pokemonForm = pokemonForms[pokemonForm];
                    //League information switch
                    switch (LeagueSelected) {
                        case "Little League":
                        case "Go Battle League LL":
                        case "CliffHanger LL":
                        case "Championship Series LL":
                        case "Mega LL":
                            //set attack to 2 if pokemon is purified and its bestIV attack stack is less than 2
                            var purifiedAttackStat = pokedexMon[0].llatkIv;
                            if (shadowOrPurifiedOrXLBuddy.includes("ic_purified")) {
                                if (pokedexMon[0].llatkIv < 2) {
                                    purifiedAttackStat = 2;
                                }
                            }
                            //check for XL and best buddy in little league
                            if (pokedexMon[0].lllevel > 41) {
                                shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/xlgraphic.png\" class=\"XLDiv\"/>";
                                if (pokedexMon[0].lllevel == 51 || pokedexMon[0].lllevel == 50.5) { shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/buddy_crown_icon.png\" class=\"BestBuddy\"/>"; }
                            }
                            //Get Ivs and add them to the html string to be displayed
                            displayBestIVs = "<div class='BestIVDiv'>CP: " + pokedexMon[0].llcP + " LV: " + pokedexMon[0].lllevel + " <span class='IVStyle'>" + purifiedAttackStat + "/" + pokedexMon[0].lldefIv + "/" + pokedexMon[0].llstaIv + "</span></div>";
                            break;
                        case "Great League":
                        case "Great League Remix":
                        case "Kanto Cup":
                        case "Holiday Cup":
                        case "Sinnoh Cup":
                        case "Love Cup":
                        case "River Cup":
                        case "Retro Cup":
                        case "Go Battle League GL":
                        case "Forged":
                        case "Firefly Cup":
                        case "Colony Cup":
                        case "CliffHanger GL":
                        case "Championship Series GL":
                        case "Alchemy Cup":
                        case "Fossil Cup":
                        case "Mega GL":
                            //set attack to 2 if pokemon is purified and its bestIV attack stack is less than 2
                            var purifiedAttackStat = pokedexMon[0].glatkIv;
                            if (shadowOrPurifiedOrXLBuddy.includes("ic_purified")) {
                                if (pokedexMon[0].glatkIv < 2) {
                                    purifiedAttackStat = 2;
                                }
                            }
                            //check for XL and best buddy in great league
                            if (pokedexMon[0].gllevel > 41) {
                                shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/xlgraphic.png\" class=\"XLDiv\"/>";
                                if (pokedexMon[0].gllevel == 51 || pokedexMon[0].gllevel == 50.5) { shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/buddy_crown_icon.png\" class=\"BestBuddy\"/>"; }
                            }
                            //Get Ivs and add them to the html string to be displayed
                            displayBestIVs = "<div class='BestIVDiv'>CP: " + pokedexMon[0].glcP + " LV: " + pokedexMon[0].gllevel + " <span class='IVStyle'>" + purifiedAttackStat + "/" + pokedexMon[0].gldefIv + "/" + pokedexMon[0].glstaIv + "</span></div>";
                            break;
                        case "Great League Premier Classic":
                            //set attack to 2 if pokemon is purified and its bestIV attack stack is less than 2
                            var purifiedAttackStat = pokedexMon[0].glclassicatkIv;
                            if (shadowOrPurifiedOrXLBuddy.includes("ic_purified")) {
                                if (pokedexMon[0].glclassicatkIv < 2) {
                                    purifiedAttackStat = 2;
                                }
                            }
                            //check for best buddy in great league classic
                            if (pokedexMon[0].glclassiclevel == 41 || pokedexMon[0].glclassiclevel == 40.5) { shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/buddy_crown_icon.png\" class=\"BestBuddy\"/>"; }
                            //Get Ivs and add them to the html string to be displayed
                            displayBestIVs = "<div class='BestIVDiv'>CP: " + pokedexMon[0].glclassiccP + " LV: " + pokedexMon[0].glclassiclevel + " <span class='IVStyle'>" + purifiedAttackStat + "/" + pokedexMon[0].glclassicdefIv + "/" + pokedexMon[0].glclassicstaIv + "</span></div>";
                            break;
                        case "Ultra League":
                        case "Ultra League Remix":
                        case "Ultra League Premier":
                        case "Go Battle League UL":
                        case "CliffHanger UL":
                        case "Championship Series UL":
                        case "Mega UL":
                            //set attack to 2 if pokemon is purified and its bestIV attack stack is less than 2
                            var purifiedAttackStat = pokedexMon[0].ulatkIv;
                            if (shadowOrPurifiedOrXLBuddy.includes("ic_purified")) {
                                if (pokedexMon[0].ulatkIv < 2) {
                                    purifiedAttackStat = 2;
                                }
                            }
                            //check for XL and best buddy  inultra league
                            if (pokedexMon[0].ullevel > 41) {
                                shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/xlgraphic.png\" class=\"XLDiv\"/>";
                                if (pokedexMon[0].ullevel == 51 || pokedexMon[0].ullevel == 50.5) { shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/buddy_crown_icon.png\" class=\"BestBuddy\"/>"; }
                            }
                            //Get Ivs and add them to the html string to be displayed
                            displayBestIVs = "<div class='BestIVDiv'>CP: " + pokedexMon[0].ulcP + " LV: " + pokedexMon[0].ullevel + " <span class='IVStyle'>" + purifiedAttackStat + "/" + pokedexMon[0].uldefIv + "/" + pokedexMon[0].ulstaIv + "</span></div>";
                            break;
                        case "Ultra League Premier Classic":
                            //set attack to 2 if pokemon is purified and its bestIV attack stack is less than 2
                            var purifiedAttackStat = pokedexMon[0].ulclassicatkIv;
                            if (shadowOrPurifiedOrXLBuddy.includes("ic_purified")) {
                                if (pokedexMon[0].ulclassicatkIv < 2) {
                                    purifiedAttackStat = 2;
                                }
                            }
                            //check for best buddy in ultra league
                            if (pokedexMon[0].ulclassiclevel == 41 || pokedexMon[0].ulclassiclevel == 40.5) { shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/buddy_crown_icon.png\" class=\"BestBuddy\"/>"; }
                            //Get Ivs and add them to the html string to be displayed
                            displayBestIVs = "<div class='BestIVDiv'>CP: " + pokedexMon[0].ulclassiccP + " LV: " + pokedexMon[0].ulclassiclevel + " <span class='IVStyle'>" + purifiedAttackStat + "/" + pokedexMon[0].ulclassicdefIv + "/" + pokedexMon[0].ulclassicstaIv + "</span></div>";
                            break;
                        case "Master League":
                        case "Master League Premier":
                        case "Go Battle League ML":
                        case "CliffHanger ML":
                        case "Championship Series ML":
                        case "Mega ML":
                            //add XL and best buddy in Master league
                            shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/xlgraphic.png\" class=\"XLDiv\"/>" + "<img src=\"../images/Pokemon/buddy_crown_icon.png\" class=\"BestBuddy\"/>";
                            displayBestIVs = "<div class='BestIVDiv'>CP: " + "MAXED" + " LV: " + 51 + " <span class='IVStyle'>" + 15 + "/" + 15 + "/" + 15 + "</span></div>";
                            break;
                        case "Master League Classic":
                        case "Master League Premier Classic":
                            shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/buddy_crown_icon.png\" class=\"BestBuddy\"/>";
                            displayBestIVs = "<div class='BestIVDiv'>CP: " + "MAXED" + " LV: " + 41 + " <span class='IVStyle'>" + 15 + "/" + 15 + "/" + 15 + "</span></div>";
                            break;
                        /*default:
                            alert('Default case');*/
                    }
                }
                else {
                    ThePokemonsID ="000";
                    pokemonForm = "00";
                    speciesNameString = speciesNameString + "(MISSING)";
                    PokemonNameDiv = "<div class = 'PokemonNameDiv'>" + speciesNameString + "</div>";
                }
                var picture = "../images/Pokemon/pokemon_icon_" + ThePokemonsID + "_" + pokemonForm + "_shiny.png";
                var li = "<li><a id=\"" + speciesNameString + "\" href=\"#\"><div class='MainBoxUL' id=\"" + ThePokemonsID + "\"><img class=\"PokemonImage\" src=\"" + picture + "\"/>" + shadowOrPurifiedOrXLBuddy + displayBestIVs + countDiv + PokemonNameDiv + "</div></a></li>";
                //dynamically paste the html into the view 
                $(".MainBox ul").append(li);

                //increase count
                count++;
            }
        }); 
    });
}


