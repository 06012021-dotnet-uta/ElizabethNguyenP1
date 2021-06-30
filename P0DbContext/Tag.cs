using System;
using System.Collections.Generic;

#nullable disable

namespace P0DbContext
{
    public partial class Tag
    {
        public Tag()
        {
            ProductTags = new HashSet<ProductTag>();
        }

        public int TagId { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }

        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
