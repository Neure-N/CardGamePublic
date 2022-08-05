using System;
using System.Collections.Generic;

namespace ProdGameApplication.Contexts
{
    public partial class CardsToDeck
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int DeckId { get; set; }

        public virtual Card Card { get; set; } = null!;
        public virtual Deck Deck { get; set; } = null!;
    }
}
