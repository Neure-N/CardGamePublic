using Microsoft.AspNetCore.Identity;

namespace ProdGameApplication.Contexts
{
    public partial class Deck
    {
        public Deck()
        {
            CardsToDecks = new HashSet<CardsToDeck>();
        }

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Errors { get; set; }

        public virtual IdentityUser User { get; set; } = null!;
        public virtual ICollection<CardsToDeck> CardsToDecks { get; set; }
    }
}
