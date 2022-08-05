using System;
using System.Collections.Generic;

namespace ProdGameApplication.Contexts
{
    public partial class DiceSymbol
    {
        public DiceSymbol()
        {
            DiceSymbolsToCards = new HashSet<DiceSymbolsToCard>();
        }

        public int Id { get; set; }
        public short Value { get; set; }
        public short SymbolType { get; set; }
        public bool? IsModifier { get; set; }
        public short Cost { get; set; }

        public virtual ICollection<DiceSymbolsToCard> DiceSymbolsToCards { get; set; }
    }
}
