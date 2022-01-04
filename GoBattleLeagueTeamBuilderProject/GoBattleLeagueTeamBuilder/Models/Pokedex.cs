using System;
using System.Collections.Generic;

#nullable disable

namespace GoBattleLeagueTeamBuilder.Models
{
    public partial class Pokedex
    {
        public int Id { get; set; }
        public int? PokemonId { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }
        public int? BaseAtk { get; set; }
        public int? BaseDef { get; set; }
        public int? BaseSta { get; set; }
        public double? LlatkIv { get; set; }
        public double? LldefIv { get; set; }
        public double? LlstaIv { get; set; }
        public double? Lllevel { get; set; }
        public int? LlcP { get; set; }
        public double? LlatkStat { get; set; }
        public double? LldefStat { get; set; }
        public double? LlstaStat { get; set; }
        public double? LlstatProduct { get; set; }
        public double? GlatkIv { get; set; }
        public double? GldefIv { get; set; }
        public double? GlstaIv { get; set; }
        public double? Gllevel { get; set; }
        public int? GlcP { get; set; }
        public double? GlatkStat { get; set; }
        public double? GldefStat { get; set; }
        public double? GlstaStat { get; set; }
        public double? GlstatProduct { get; set; }
        public double? UlatkIv { get; set; }
        public double? UldefIv { get; set; }
        public double? UlstaIv { get; set; }
        public double? Ullevel { get; set; }
        public int? UlcP { get; set; }
        public double? UlatkStat { get; set; }
        public double? UldefStat { get; set; }
        public double? UlstaStat { get; set; }
        public double? UlstatProduct { get; set; }
        public double? UlclassicatkIv { get; set; }
        public double? UlclassicdefIv { get; set; }
        public double? UlclassicstaIv { get; set; }
        public double? Ulclassiclevel { get; set; }
        public int? UlclassiccP { get; set; }
        public double? UlclassicatkStat { get; set; }
        public double? UlclassicdefStat { get; set; }
        public double? UlclassicstaStat { get; set; }
        public double? UlclassicstatProduct { get; set; }
    }
}
