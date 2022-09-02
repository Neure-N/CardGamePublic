using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProdGameApplication.Contexts
{
    public partial class GameContext : IdentityDbContext<IdentityUser>
    {
        public GameContext()
        {
        }

        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<CardsToDeck> CardsToDecks { get; set; } = null!;
        public virtual DbSet<CardsToUser> CardsToUsers { get; set; } = null!;
        public virtual DbSet<Combat> Combats { get; set; } = null!;
        public virtual DbSet<Count> Counts { get; set; } = null!;
        public virtual DbSet<CountCategory> CountCategories { get; set; } = null!;
        public virtual DbSet<Deck> Decks { get; set; } = null!;
        public virtual DbSet<DiceSymbol> DiceSymbols { get; set; } = null!;
        public virtual DbSet<DiceSymbolsToCard> DiceSymbolsToCards { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("u1338927_gameAdm");

            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.Color).HasDefaultValueSql("(CONVERT([smallint],(0)))");

                entity.Property(e => e.IsUnique)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");
            });

            modelBuilder.Entity<CardsToDeck>(entity =>
            {
                entity.HasOne(d => d.Card)
                    .WithMany(p => p.CardsToDecks)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CardsToDecks_fk");

                entity.HasOne(d => d.Deck)
                    .WithMany(p => p.CardsToDecks)
                    .HasForeignKey(d => d.DeckId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CardsToDecks_fk2");
            });

            modelBuilder.Entity<Combat>(entity =>
            {
                entity.Property(e => e.User1Id).HasMaxLength(450);

                entity.Property(e => e.User2Id).HasMaxLength(450);

                //entity.HasOne(d => d.User1)
                //    .WithMany(p => p.CombatUser1s)
                //    .HasForeignKey(d => d.User1Id)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("Combats_fk");

                //entity.HasOne(d => d.User2)
                //    .WithMany(p => p.CombatUser2s)
                //    .HasForeignKey(d => d.User2Id)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("Combats_fk2");
            });

            modelBuilder.Entity<Count>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Value).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId);
            });

            modelBuilder.Entity<Deck>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.UserId).HasMaxLength(450);

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.Decks)
                //    .HasForeignKey(d => d.UserId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("Decks_fk");
            });

            modelBuilder.Entity<DiceSymbol>(entity =>
            {
                entity.Property(e => e.IsModifier)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.SymbolType).HasDefaultValueSql("(CONVERT([smallint],(0)))");

                entity.Property(e => e.Value).HasDefaultValueSql("(CONVERT([smallint],(0)))");
            });

            modelBuilder.Entity<DiceSymbolsToCard>(entity =>
            {
                entity.HasOne(d => d.Card)
                    .WithMany(p => p.DiceSymbolsToCards)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DiceSymbolsToCards_fk2");

                entity.HasOne(d => d.DiceSymbol)
                    .WithMany(p => p.DiceSymbolsToCards)
                    .HasForeignKey(d => d.DiceSymbolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DiceSymbolsToCards_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
