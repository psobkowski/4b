using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mulder.DataAccess.Models
{
    public partial class MulderContext : DbContext
    {
        public MulderContext()
        {
        }

        public MulderContext(DbContextOptions<MulderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Matches> Matches { get; set; }
        public virtual DbSet<MatchesLineUp> MatchesLineUp { get; set; }
        public virtual DbSet<MatchesScore> MatchesScore { get; set; }
        public virtual DbSet<MatchesSpectators> MatchesSpectators { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<PlayersScore> PlayersScore { get; set; }
        public virtual DbSet<Spectators> Spectators { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SPN-PC002;Database=Mulder;user id=sa;password=sa");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matches>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<MatchesLineUp>(entity =>
            {
                entity.HasIndex(e => new { e.MatchId, e.TeamId, e.PlayerId })
                    .HasName("UQ_Matches_Teams_Players")
                    .IsUnique();

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchesLineUp)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchesLineUp_Matches");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.MatchesLineUp)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchesLineUp_Players");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.MatchesLineUp)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchesLineUp_Teams");
            });

            modelBuilder.Entity<MatchesScore>(entity =>
            {
                entity.HasIndex(e => new { e.MatchId, e.TeamId })
                    .HasName("UQ_Matches_Teams")
                    .IsUnique();

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchesScore)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchesScore_Matches");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.MatchesScore)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchesScore_Teams");
            });

            modelBuilder.Entity<MatchesSpectators>(entity =>
            {
                entity.HasIndex(e => new { e.MatchId, e.SpectatorId })
                    .HasName("UQ_Matches_Spectators")
                    .IsUnique();

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchesSpectators)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchesSpectators_Matches");

                entity.HasOne(d => d.Spectator)
                    .WithMany(p => p.MatchesSpectators)
                    .HasForeignKey(d => d.SpectatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchesSpectators_Spectators");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.NickName).HasMaxLength(10);

                entity.Property(e => e.Number).HasMaxLength(4);

                entity.HasOne(d => d.CurrentTeam)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.CurrentTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Players_Teams");
            });

            modelBuilder.Entity<Spectators>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);
            });
        }
    }
}
