using System;
using System.Collections.Generic;

namespace ProdGameApplication.Contexts
{
    public partial class Count
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Value { get; set; }
        public int CategoryId { get; set; }

        public virtual CountCategory Category { get; set; } = null!;
    }
}
