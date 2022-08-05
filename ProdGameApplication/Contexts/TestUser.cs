using System;
using System.Collections.Generic;

namespace ProdGameApplication.Contexts
{
    public partial class TestUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
