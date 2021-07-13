using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P0DbContext;

namespace P1
{
    /// <summary>
    /// To Aggregate Order Information for display
    /// </summary>
    class DetailedOrder
    {
        Fetch Fetch = new Fetch();
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime OrderDate { get; set; }
        public Product Product { get; set; }

        public DetailedOrder(Order order, OrderedProduct product)
        {
            this.OrderId = order.OrderId;
            this.ProductId = product.ProductId;
            this.Amount = product.Amount;
            this.CustomerId = order.CustomerId;
            this.LocationId = order.LocationId;
            this.OrderDate = order.OrderDate;
            this.Product = Fetch.ProductFromProductID(product.ProductId);

        }

    }
}
