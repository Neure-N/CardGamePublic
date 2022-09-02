using ProdGameApplication.Enums;

namespace ProdGameApplication.Models.Game
{
    public class UiCard
    {
        public short? Cost { get; set; }
        public Enums.Type Type { get; set; }
        public Subtype Subtype1 { get; set; }
        public Subtype Subtype2 { get; set; }
        public Subtype Subtype3 { get; set; }
        public Color Color { get; set; }
        public bool IsUnique { get; set; }
        public string? Name { get; set; }
        public string? Ability { get; set; }
        public string? Description { get; set; }
        public short? Health { get; set; }
        public bool IsActivated { get; set; }

        public List<UiDice>? Dices { get; set; }
    }
}
