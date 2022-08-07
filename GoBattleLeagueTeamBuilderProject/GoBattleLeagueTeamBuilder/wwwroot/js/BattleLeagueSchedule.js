$(document).ready(function () {
    $('#BattleLeagueSeasonDetails').click(function () {
        var btn = document.getElementById("BattleLeagueSeasonDetails");
        var div = document.getElementById("openBattleLeagueSeasonDetails");
            if (div.style.display === "none") {
                div.style.display = "block";
            }
            else {
                div.style.display = "none";
            }
    });
});