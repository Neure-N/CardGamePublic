using System;
using System.Collections.Generic;

namespace ProdGameApplication.Contexts
{
    public partial class Card
    {
        public Card()
        {
            CardsToDecks = new HashSet<CardsToDeck>();
            DiceSymbolsToCards = new HashSet<DiceSymbolsToCard>();
        }

        public int Id { get; set; }
        public short? Cost { get; set; }
        public short Affiliation { get; set; }
        public short Type { get; set; }
        public short? Subtype1 { get; set; }
        public short? Subtype2 { get; set; }
        public short? Subtype3 { get; set; }
        public short Color { get; set; }
        public bool? IsUnique { get; set; }
        public string Name { get; set; } = null!;
        public string? Ability { get; set; }
        public string? SpecialDescription { get; set; }
        public string Description { get; set; } = null!;
        public short? NonElitePointValue { get; set; }
        public short? ElitePointValue { get; set; }
        public short? Health { get; set; }
        public string? LoreDescription { get; set; }

        public virtual ICollection<CardsToDeck> CardsToDecks { get; set; }
        public virtual ICollection<DiceSymbolsToCard> DiceSymbolsToCards { get; set; }
    }
}
