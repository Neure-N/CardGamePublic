using Microsoft.AspNetCore.Identity;

namespace ProdGameApplication.Contexts
{
    public partial class Combat
    {
        public int Id { get; set; }
        public string User1Id { get; set; } = null!;
        public string User2Id { get; set; } = null!;
        public int? WinnerId { get; set; }
        public int Points { get; set; }

        public virtual IdentityUser User1 { get; set; } = null!;
        public virtual IdentityUser User2 { get; set; } = null!;
    }
}
