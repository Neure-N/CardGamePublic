using System;
using System.Collections.Generic;

namespace ProdGameApplication.Contexts
{
    public partial class ExtendedUser
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? About { get; set; }
        public string? Image { get; set; }
    }
}
