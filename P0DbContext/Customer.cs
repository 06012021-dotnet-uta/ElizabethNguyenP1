using System;
using System.Collections.Generic;

#nullable disable

namespace P0DbContext
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public int Age { get; set; }
        public bool? SecretUser { get; set; }
        public int? DefaultStore { get; set; }

        public virtual Location DefaultStoreNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
