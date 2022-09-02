using ProdGameApplication.Contexts;

namespace ProdGameApplication.Models.Game
{
    public class UiDice
    {
        public string? UiCardType { get; set; }
        public List<DiceSymbol>? DiceSymbols { get; set; }
        public DiceSymbol? RolledSymbol { get; set; }
    }
}
