using System;
using System.Collections.Generic;

namespace ProdGameApplication.Contexts
{
    public partial class DiceSymbolsToCard
    {
        public int Id { get; set; }
        public int DiceSymbolId { get; set; }
        public int CardId { get; set; }

        public virtual Card Card { get; set; } = null!;
        public virtual DiceSymbol DiceSymbol { get; set; } = null!;
    }
}
