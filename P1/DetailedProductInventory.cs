using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P0DbContext;

namespace P1
{
    /// <summary>
    /// This class is used to aggregate/join data from Products and Inventory Classes.
    /// </summary>
    public class DetailedProductInventory
    {
        Fetch Fetch = new Fetch();
        public Product Product;
        public int LocationId;
        public int Inventory;
        public DetailedProductInventory(Location location, Product product)
        {
            Product = product;
            LocationId = location.LocationId;
            Inventory = Fetch.InventoryFromProductID(product.ProductId, LocationId);
        }
        public DetailedProductInventory(int locationId, int productId)
        {
            this.Product = Fetch.ProductFromProductID(productId);
            this.Inventory = Fetch.InventoryFromProductID(productId, locationId);
            this.LocationId = locationId;
        }

        public DetailedProductInventory(int locationId, int productId, int amount)
        {
            this.Product = Fetch.ProductFromProductID(productId);
            this.Inventory = amount;
            this.LocationId = locationId;
        }
    }
}
