using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GoBattleLeagueTeamBuilder.Models
{
    public partial class GoBattleLeagueTeamBuilderDBContext : DbContext
    {
        public GoBattleLeagueTeamBuilderDBContext()
        {
        }

        public GoBattleLeagueTeamBuilderDBContext(DbContextOptions<GoBattleLeagueTeamBuilderDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pokedex> Pokedexes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=GoBattleLeageTeamBuilderConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Pokedex>(entity =>
            {
                entity.ToTable("Pokedex");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Form).HasMaxLength(60);

                entity.Property(e => e.GlatkIv).HasColumnName("GLatkIV");

                entity.Property(e => e.GlatkStat).HasColumnName("GLatkSTAT");

                entity.Property(e => e.GlcP).HasColumnName("GLcP");

                entity.Property(e => e.GlclassicatkIv).HasColumnName("GLClassicatkIV");

                entity.Property(e => e.GlclassicatkStat).HasColumnName("GLClassicatkSTAT");

                entity.Property(e => e.GlclassiccP).HasColumnName("GLClassiccP");

                entity.Property(e => e.GlclassicdefIv).HasColumnName("GLClassicdefIV");

                entity.Property(e => e.GlclassicdefStat).HasColumnName("GLClassicdefSTAT");

                entity.Property(e => e.Glclassiclevel).HasColumnName("GLClassiclevel");

                entity.Property(e => e.GlclassicstaIv).HasColumnName("GLClassicstaIV");

                entity.Property(e => e.GlclassicstaStat).HasColumnName("GLClassicstaSTAT");

                entity.Property(e => e.GlclassicstatProduct).HasColumnName("GLClassicstatProduct");

                entity.Property(e => e.GldefIv).HasColumnName("GLdefIV");

                entity.Property(e => e.GldefStat).HasColumnName("GLdefSTAT");

                entity.Property(e => e.Gllevel).HasColumnName("GLlevel");

                entity.Property(e => e.GlstaIv).HasColumnName("GLstaIV");

                entity.Property(e => e.GlstaStat).HasColumnName("GLstaSTAT");

                entity.Property(e => e.GlstatProduct).HasColumnName("GLstatProduct");

                entity.Property(e => e.LlatkIv).HasColumnName("LLatkIV");

                entity.Property(e => e.LlatkStat).HasColumnName("LLatkSTAT");

                entity.Property(e => e.LlcP).HasColumnName("LLcP");

                entity.Property(e => e.LldefIv).HasColumnName("LLdefIV");

                entity.Property(e => e.LldefStat).HasColumnName("LLdefSTAT");

                entity.Property(e => e.Lllevel).HasColumnName("LLlevel");

                entity.Property(e => e.LlstaIv).HasColumnName("LLstaIV");

                entity.Property(e => e.LlstaStat).HasColumnName("LLstaSTAT");

                entity.Property(e => e.LlstatProduct).HasColumnName("LLstatProduct");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.PokemonId).HasColumnName("PokemonID");

                entity.Property(e => e.UlatkIv).HasColumnName("ULatkIV");

                entity.Property(e => e.UlatkStat).HasColumnName("ULatkSTAT");

                entity.Property(e => e.UlcP).HasColumnName("ULcP");

                entity.Property(e => e.UlclassicatkIv).HasColumnName("ULClassicatkIV");

                entity.Property(e => e.UlclassicatkStat).HasColumnName("ULClassicatkSTAT");

                entity.Property(e => e.UlclassiccP).HasColumnName("ULClassiccP");

                entity.Property(e => e.UlclassicdefIv).HasColumnName("ULClassicdefIV");

                entity.Property(e => e.UlclassicdefStat).HasColumnName("ULClassicdefSTAT");

                entity.Property(e => e.Ulclassiclevel).HasColumnName("ULClassiclevel");

                entity.Property(e => e.UlclassicstaIv).HasColumnName("ULClassicstaIV");

                entity.Property(e => e.UlclassicstaStat).HasColumnName("ULClassicstaSTAT");

                entity.Property(e => e.UlclassicstatProduct).HasColumnName("ULClassicstatProduct");

                entity.Property(e => e.UldefIv).HasColumnName("ULdefIV");

                entity.Property(e => e.UldefStat).HasColumnName("ULdefSTAT");

                entity.Property(e => e.Ullevel).HasColumnName("ULlevel");

                entity.Property(e => e.UlstaIv).HasColumnName("ULstaIV");

                entity.Property(e => e.UlstaStat).HasColumnName("ULstaSTAT");

                entity.Property(e => e.UlstatProduct).HasColumnName("ULstatProduct");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
