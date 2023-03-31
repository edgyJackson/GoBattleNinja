//Data and variables needed for processing pokemon information
var speciesNameRegEx = new RegExp("/^\S*/"); 
var speciesFormRegEx = new RegExp("/\(.+?\)/");
var pokemonForms = { "PRIMAL": 53,"MEGA_X": 51, "MEGA_Y": 52, "MEGA": 51, "PIKACHU_FLYING":99,"STEEL":11,"ROCK":11,"PSYCHIC":11,"POISON":11,"ICE":11,"GROUND":11,"GRASS":11,"GHOST":11,"FIRE":11,"FIGHTING":11,"FAIRY":11,"ELECTRIC":11,"DRAGON":11,"DARK":11,"BUG":11,"ORIGINAL_COLOR":11,"ULTRA":11,"DUSK_MANE":11,"DAWN_WINGS":11,"YELLOW":20,"VIOLET":11,"RED":18,"ORANGE":17,"BUSTED":11,"INDIGO":11,"BLUE":16,"GREEN":11,"DISGUISED":11,"HANGRY":11,"NOICE":11,"FULL_BELLY":11,"CROWNED_SWORD":11,"ICE_RIDER":11,"SINGLE_STRIKE":11,"RAPID_STRIKE":11,"ETERNAMAX":11,"CROWNED_SHIELD":11,"PHONY": 11,"ANTIQUE": 11,"LOW_KEY": 11,"AMPED": 11,"STAR": 11,"PHARAOH": 11,"NATURAL": 11,"MATRON": 11,"LA_REINE": 11,"KABUKI": 11,"HEART": 11,"DIAMOND": 11, "DEBUTANTE": 11,"DANDY":11,"SOLO": 11, "SCHOOL": 11,"MIDNIGHT": 11, "MIDDAY": 12,"DUSK":11,"CONFINED":12,"SENSU": 16,"PAU": 19, "POM_POM": 17,"BAILE":18,"ALOLA":61,"HISUIAN":72,"COSTUME_2020":11,"A":11,"2021":11,"VS_2019":11,"FLYING_5TH_ANNIV":11,"COPY_2019": 11, "FALL_2019": 11,"ADVENTURE_HAT_2020":11,"FLYING_OKINAWA":11,"2020": 11,"2022": 11,"S": 11,"SHADOW_RIDER":11,"WATER":21,"EAST_SEA": 12,"WEST_SEA":11,"WINTER_2020": '00',"RED_STRIPED": 11, "BLUE_STRIPED": 12,"GALARIAN_ZEN": 12,"GALARIAN_STANDARD":31,"NORMAL":'00',"GALARIAN": 31, "DEFENSE": 13, "SHADOW": '00', "ALOLAN": 61, "SNOWY": 14, "RAINY": 13, "SUNNY": 12, "ATTACK": 12, "SPEED": 14, "PLANT": 11, "SANDY": 12, "TRASH": 13, "OVERCAST": 11, "SUNSHINE": 12, "WEST": 11, "EAST": 12, "REGULAR": 11, "HEAT": 12, "WASH": 13, "FAN": 15, "FROST": 14, "MOW": 15, "ORIGIN": 12, "ALTERED": 11, "LAND": 11, "SKY": 12, "STANDARD": 11, "ZEN": 12, "SPRING": 11, "SUMMER": 12, "AUTUMN": 13, "WINTER": 14, "INCARNATE": 11, "THERIAN": 12, "WHITE": 12, "BLACK": 13, "ORDINARY": 11, "RESOLUTE": 12, "ARIA": 11, "PIROUETTE": 12, "DOUSE": 12, "BURN": 14, "SHOCK": 13, "CHILL": 15, "ARMORED": 50, "HERO": 11, "UNBOUND": 11, "AVERAGE": 11, "LARGE": 12, "SMALL": 13, "SUPER": 14, "MALE": '00', "FEMALE": '01', "LIBRE": 16, "5TH_ANNIVERSARY": 12, "FLYING": 11, "KARIYUSHI": 13, "ROCK_STAR": 14, "POP_STAR": 15, "JR": '00', "NO_FORM":'00'};
var LeagueDictionary = { "Fantasy Cup: Ultra League Edition": "2500", "Catch Cup: Rising Heroes Edition": "1500", "Sunshine Cup: Great League Edition": "1500", "Spring Cup: Great League Edition": "1500", "Mountain Cup: Great League Edition": "1500", "Color Cup: Great League Edition": "1500", "Naiad Cup": "1500", "Justicar Cup": "2500", "Polkadot Cup": "1500", "Ionic Cup": "1500", "Hoenn Cup": "1500", "Electric Cup": "1500", "Love Cup": "1500", "Weather Cup GL": "1500", "Ultra League Holiday Cup": "2500", "WillPower Cup": "1500", "Halloween Cup UL": "2500", "Halloween Cup GL": "1500", "Evolution Cup": "1500", "Weather Cup UL": "2500", "Psychic Cup": "1500", "Little Jungle Cup Remix": "500", "Fighting Cup": "1500", "Summer Cup": "1500", "Element Cup": "500", "Hisui Cup": "1500", "Flying Cup": "1500", "Remix: Little League Edition": "500", "Fossil Cup": "1500", "Go Battle League ML": "10000", "CliffHanger ML": "10000", "Championship Series ML": "10000", "Master League: Mega Edition": "10000", "Go Battle League UL": "2500", "CliffHanger UL": "2500", "Championship Series UL": "2500", "Ultra League: Mega Edition": "2500", "CliffHanger LL": "500", "Championship Series LL": "500", "Go Battle League LL": "500", "Little League: Mega Edition": "500", "CliffHanger GL": "1500", "Great League: Mega Edition": "1500", "Retro Cup": "1500", "Championship Series GL": "1500", "Go Battle League GL": "1500", "Forged Cup": "1500", "Firefly Cup": "1500", "Colony Cup": "1500", "Alchemy Cup": "1500", "River Cup": "1500", "Little League": "500", "Great League": "1500", "Remix: Great League Edition": "1500", "Kanto Cup": "1500", "Sinnoh Cup": "1500", "Holiday Cup": "1500", "Ultra League": "2500", "Remix: Ultra League Edition": "2500", "Premier: Ultra League Edition": "2500", "Premier Classic: Ultra League Edition": "2500", "Master League": "10000", "Classic: Master League Edition": "10000", "Premier: Master League Edition": "10000", "Premier Classic: Little League Edition": "500", "Premier Classic: Great League Edition": "1500", "Premier Classic: Master League Edition": "10000", "Love Cup": "1500" };
var WeirdNameList = ["HAKAMO_O", "HO_OH", "JANGMO_O", "KOMMO_O", "PORYGON_Z", "MIME_JR", "MR_MIME", "MR_RIME", "TAPU_BULU", "TAPU_FINI", "TAPU_KOKO", "TAPU_LELE", "TYPE_NULL","NIDORAN_MALE","NIDORAN_FEMALE"];
var NoHyphenList = ["MIME_JR", "MR_MIME", "MR_RIME", "TAPU_BULU", "TAPU_FINI", "TAPU_KOKO", "TAPU_LELE", "TYPE_NULL"];
$(document).ready(function () {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Home/GetPokedex/",
        success: SelectLeague,
        error: errorOnAjax
    });
});

function errorOnAjax() {
    console.log("ERROR in ajax request. Probably wrong database");
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
        a = li[i].getElementsByTagName("div")[0];
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

        let jsonString = "https://raw.githubusercontent.com/pvpoke/pvpoke/master/src/data/rankings/" + this.id + "/overall/rankings-" + LeagueDictionary[this.textContent] + ".json";

        document.getElementById("headerText").innerText = this.textContent;
        var LeagueSelected = this.textContent;
        //for shiny button
        var shiny = "";
        $.getJSON(jsonString, function (data) {
            console.log("This list is " + data.length + " long!");
            $(".MainBox ul").empty();
            var count = 1;
            for (i = 0; i < data.length; i++) {
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
                var PokemonTypeDiv1 = "";
                var PokemonTypeDiv2 = "";
                var PokemonType1 = "";
                var PokemonType2 = "";
                var move1 = data[i].moveset[0];
                var move2 = data[i].moveset[1];
                var move3 = data[i].moveset[2]??"";
                //1: get the pokemon name that will be displayed in the html mainbox div and check for purified =========
                speciesNameString = data[i].speciesName;
                if (data[i].moveset.includes("RETURN")) {
                    speciesNameString = speciesNameString + " (Purified)";
                    shadowOrPurifiedOrXLBuddy = "<img src=\"../images/Pokemon/ic_purified.png\" class=\"ShadowOrPurified\"/>";
                }
                PokemonNameDiv = "<div class = 'PokemonNameDiv'>" + speciesNameString + "</div>";
                //2: get the speciesId from pvpoke json and set it to uppercase; for finding the pokemon in the pokedex/database
                //also set _XS status  =======================================================================
                speciesID = data[i].speciesId.toUpperCase();
                if (speciesID.endsWith("_XS")) {
                    speciesID = speciesID.replace("_XS", "");
                    if (!(LeagueSelected.includes("LL") || (LeagueSelected.includes("Little")))) {
                        LeagueSelected = "Great League Premier Classic";
                    }
                    console.log(data[i].speciesId);
                }
                //3: Check if pokemon is a shadow form and fill shadowOrPurifiedOrXLBuddy if true, then remove the shadow from the 
				        //speciesId  ======================================================================================
                if (speciesID.endsWith("_SHADOW")) {
                    shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/ic_shadow.png\" class=\"ShadowOrPurified\"/>";
                    speciesID=speciesID.replace("_SHADOW", "");
                }
				        //4: Get form from speciesId  =======================================================================
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
                //For debugging
                if(data[i].speciesId.toUpperCase() == "MAGNEZONE") {
                    console.log("hi");
                }
                var pokedexMon;
                //5: get the pokemon from the pokedex  ======================[MAJOR VALOTILE ZONE]=====================================
                if (isWeirdName) {
                    pokedexMon = pokedex.listPokedex.filter(obj => obj.name == pokemonName && obj.form == pokemonForm2);
                } else {
                    pokedexMon = pokedex.listPokedex.filter(obj => obj.name == pokemonName && obj.form == pokemonForm);
                }
                ThePokemonsID = pokedexMon[0].pokemonId.toString().padStart(3, "0");
                var pokemonFormForFindingPokemonInPokemonDataList = "";
                if (pokemonForm != "NO_FORM" && !pokemonForm.includes("MEGA") && !pokemonForm.includes("PRIMAL")) {
                    if (isWeirdName) {
                        pokemonFormForFindingPokemonInPokemonDataList = "_" + pokemonForm2;
                    } else {
                        pokemonFormForFindingPokemonInPokemonDataList = "_" + pokemonForm;
                    }
                }
                //6: get the pokemon from the pokemonDataLists  =======================================================
                if (pokemonName.includes("NIDORAN")) {
                    var pokemonDataListMon = pokedex.pokemonDataLists.listPokemonSettings.filter(obj => obj.pokemonSettings.pokemonId.includes(pokemonName + pokemonFormForFindingPokemonInPokemonDataList))[0];
                } else {
                    var pokemonDataListMon = pokedex.pokemonDataLists.listPokemonSettings.filter(obj => obj.templateId.includes(pokemonName + pokemonFormForFindingPokemonInPokemonDataList) && obj.templateId.includes(ThePokemonsID))[0];
                }
                //7: Get the pokemon moves and format them =============================================================
                //check for legacy moves
                if (pokemonDataListMon.pokemonSettings.eliteQuickMove!=null) {
                    if (pokemonDataListMon.pokemonSettings.eliteQuickMove.includes(move1 +"_FAST")) {
                        move1 = move1 + "<span class='LegacyMoveStyle'> (LEGACY)</span>";
                    }
                }
                if (pokemonDataListMon.pokemonSettings.eliteCinematicMove != null) {
                    if (pokemonDataListMon.pokemonSettings.eliteCinematicMove.includes(move2)) {
                        move2 = move2 + "<span class='LegacyMoveStyle'> (LEGACY)</span>";
                    }
                    if (move3 != "" && pokemonDataListMon.pokemonSettings.eliteCinematicMove.includes(move3)) {
                        move3 = move3 + "<span class='LegacyMoveStyle'> (LEGACY)</span>";
                    }
                }
               /* //Temproary check for precipice blades and origin pulse and other unreleased legacy moves------------------------------------
                if (move2.includes("PRECIPICE_BLADES") || move2.includes("ORIGIN_PULSE")) {
                    move2 = move2 + "<span class='LegacyMoveStyle'> (LEGACY)</span>";
                }
                if (move3 != "" && move3.includes("PRECIPICE_BLADES") || move3.includes("ORIGIN_PULSE")) {
                    move3 = move3 + "<span class='LegacyMoveStyle'> (LEGACY)</span>";
                }*/
                //-----------------------------------------------------------------------------------------------
                if (move1.includes("_")) {
                    move1 = move1.replaceAll("_", " ")
                }
                if (move2.includes("_")) {
                    move2 = move2.replaceAll("_", " ")
                }
                if (move3 != null) {
                    if (move3.includes("_")) {
                        move3 = move3.replaceAll("_", " ")
                    }
                }
                var PokemonMoveDiv1 = "<div class = 'PokemonMoveDiv1'>" + move1 + "</div>";
                var PokemonMoveDiv2 = "<div class = 'PokemonMoveDiv2'>" + move2 + "</div>";
                var PokemonMoveDiv3 = "<div class = 'PokemonMoveDiv3'>" + move3 + "</div>";

                //8: Get the pokemon type and add to the div to display the types =====================================
                if (pokemonForm.includes("MEGA") || pokemonForm.includes("PRIMAL")  && pokemonDataListMon.pokemonSettings.tempEvoOverrides!=null) {
                    var pokmeonMegaVersionNumber = 0;
                    //Add check for extra mega forms here, mewtwo and charizard are the only ones so far, just add an or "||" to the if statement
                    if (pokemonForm.includes("MEGA_Y")) {
                        pokmeonMegaVersionNumber = 1;
                    }
                    PokemonType1 = pokemonDataListMon.pokemonSettings.tempEvoOverrides[pokmeonMegaVersionNumber].typeOverride1;
                    if (pokemonDataListMon.pokemonSettings.tempEvoOverrides[pokmeonMegaVersionNumber].typeOverride2!=null) {
                        PokemonType2 = pokemonDataListMon.pokemonSettings.tempEvoOverrides[pokmeonMegaVersionNumber].typeOverride2;
                    }
                } else {
                    PokemonType1 = pokemonDataListMon.pokemonSettings.type;
                    if (pokemonDataListMon.pokemonSettings.type2!=null) {
                        PokemonType2 = pokemonDataListMon.pokemonSettings.type2;
                    }
                }
                PokemonTypeDiv1 = "<img src=\"../images/Pokemon/Types/" + PokemonType1 + "_BORDERED.png\" class=\"PokemonTypeDiv1\"/>";
                if (PokemonType2!="") {
                    PokemonTypeDiv2 = "<img src=\"../images/Pokemon/Types/" + PokemonType2 + "_BORDERED.png\" class=\"PokemonTypeDiv2\"/>";
                }
				//9: Get pokemon IV's, XL and Best buddy status =======================================================
                if(pokedexMon.length == 1 && pokedexMon[0].llatkIv != null) {
                    //Use the pokemons id for the mainbox string concatenation      
                    pokemonForm = pokemonForms[pokemonForm];
                    //League information switch
                    switch (LeagueSelected) {
                        case "Little League":
                        case "Remix: Little League Edition":
                        case "Go Battle League LL":
                        case "CliffHanger LL":
                        case "Championship Series LL":
                        case "Little League: Mega Edition":
                        case "Element Cup":
                        case "Little Jungle Cup Remix":
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
                        case "Premier Classic: Little League Edition":
                            //set attack to 2 if pokemon is purified and its bestIV attack stack is less than 2
                            var purifiedAttackStat = pokedexMon[0].glclassicatkIv;
                            if (shadowOrPurifiedOrXLBuddy.includes("ic_purified")) {
                                if (pokedexMon[0].glclassicatkIv < 2) {
                                    purifiedAttackStat = 2;
                                }
                            }
                            //check for best buddy in little league classic----------------------------------------------------------------update-------------------------------------------------
                            if (pokedexMon[0].glclassiclevel == 41 || pokedexMon[0].glclassiclevel == 40.5) { shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/buddy_crown_icon.png\" class=\"BestBuddy\"/>"; }
                            //Get Ivs and add them to the html string to be displayed
                            displayBestIVs = "<div class='BestIVDiv'>CP: " + pokedexMon[0].glclassiccP + " LV: " + pokedexMon[0].glclassiclevel + " <span class='IVStyle'>" + purifiedAttackStat + "/" + pokedexMon[0].glclassicdefIv + "/" + pokedexMon[0].glclassicstaIv + "</span></div>";
                            break;
                        case "Great League":
                        case "Summer Cup":
                        case "Fighting Cup":
                        case "Remix: Little League Edition":
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
                        case "Great League: Mega Edition":
                        case "Hisui Cup":
                        case "Flying Cup":
                        case "WillPower Cup":
                        case "Psychic Cup":
                        case "Evolution Cup":
                        case "Halloween Cup GL":
                        case "Weather Cup GL":
                        case "Electric Cup":
                        case "Love Cup":
                        case "Hoenn Cup":
                        case "Naiad Cup": 
                        case "Justicar Cup": 
                        case "Polkadot Cup":
                        case "Ionic Cup":
                        case "Color Cup: Great League Edition":
                        case "Mountain Cup: Great League Edition":
                        case "Spring Cup: Great League Edition":
                        case "Sunshine Cup: Great League Edition":
                        case "Catch Cup: Rising Heroes Edition":
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
                        case "Remix: Ultra League Edition":
                        case "Premier: Ultra League Edition":
                        case "Go Battle League UL":
                        case "CliffHanger UL":
                        case "Championship Series UL":
                        case "Ultra League: Mega Edition":
                        case "Weather Cup UL":
                        case "Halloween Cup UL":
                        case "Ultra League Holiday Cup":
                        case "Fantasy Cup: Ultra League Edition":
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
                        case "Premier: Master League Edition":
                        case "Go Battle League ML":
                        case "CliffHanger ML":
                        case "Championship Series ML":
                        case "Master League: Mega Edition":
                            //add XL and best buddy in Master league
                            shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/xlgraphic.png\" class=\"XLDiv\"/>" + "<img src=\"../images/Pokemon/buddy_crown_icon.png\" class=\"BestBuddy\"/>";
                            displayBestIVs = "<div class='BestIVDiv'>CP: " + pokedexMon[0].mlcP + " LV: " + 51 + " <span class='IVStyle'>" + pokedexMon[0].mlatkIv + "/" + pokedexMon[0].mldefIv + "/" + pokedexMon[0].mlstaIv + "</span></div>";
                            break;
                        case "Classic: Master League Edition":
                        case "Premier Classic: Master League Edition":
                            shadowOrPurifiedOrXLBuddy = shadowOrPurifiedOrXLBuddy + "<img src=\"../images/Pokemon/buddy_crown_icon.png\" class=\"BestBuddy\"/>";
                            displayBestIVs = "<div class='BestIVDiv'>CP: " + pokedexMon[0].mlclassiccP + " LV: " + 41 + " <span class='IVStyle'>" + pokedexMon[0].mlclassicatkIv + "/" + pokedexMon[0].mlclassicdefIv + "/" + pokedexMon[0].mlclassicstaIv + "</span></div>";
                            break;
                        /*default:
                            alert('Default case');*/
                    }
                }
                else { //pokemon is not fully stored in the db need to update this db entry ======================
                    ThePokemonsID ="000";
                    pokemonForm = "00";
                    speciesNameString = speciesNameString + "(MISSING)";
                    PokemonNameDiv = "<div class = 'PokemonNameDiv'>" + speciesNameString + "</div>";
                } 
				//10: assemble the html and append to the mainbux ul ================================================
                var picture = "../images/Pokemon/pokemon_icon_" + ThePokemonsID + "_" + pokemonForm + ".png";
                var li = "<li><div class='MainBoxUL' id=\"" + speciesNameString + ThePokemonsID + PokemonType1 + PokemonType2 + "\" data-toggle='tooltip' data-placement='left' data-trigger='hover' data-delay='500' title='" + ThePokemonsID +"'><img class=\"PokemonImage\" src=\"" + picture + "\"/>" + shadowOrPurifiedOrXLBuddy + displayBestIVs + countDiv + PokemonNameDiv + PokemonTypeDiv1 + PokemonTypeDiv2 + PokemonMoveDiv1 + PokemonMoveDiv2 + PokemonMoveDiv3 + "</div></li>";
                //dynamically paste the html into the view 
                $(".MainBox ul").append(li);
                //increase count
                count++;
            }
        }); 
    });
}


