using Microsoft.AspNetCore.Identity;

namespace ProdGameApplication.Contexts
{
    public partial class CardsToUser
    {
        public long Id { get; set; }
        public int CardId { get; set; }
        public string UserId { get; set; } = null!;

        public virtual Card Card { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;
    }
}
