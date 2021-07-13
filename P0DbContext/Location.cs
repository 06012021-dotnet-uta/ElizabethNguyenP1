using System;
using System.Collections.Generic;

#nullable disable

namespace P0DbContext
{
    public partial class Location
    {
        public Location()
        {
            Customers = new HashSet<Customer>();
            Inventories = new HashSet<Inventory>();
            Orders = new HashSet<Order>();
        }

        public int LocationId { get; set; }
        public string LocationAddress { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
