using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P0DbContext;

namespace P1
{
    public class Fetch
    {

        public Fetch()
        {
            this.context = new P0_DatabaseContext();
        }

        public Fetch(P0_DatabaseContext context)
        {
            this.context = context;
        }

        public P0_DatabaseContext context; //= new P0_DatabaseContext();

        /// <summary>
        /// LocationInfo returns the location by location id
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns>Location</returns>
        public Location LocationInfo(int locationId)
        {
            Location result = new Location();
            List<Location> place = context.Locations.Where(row => row.LocationId == locationId).ToList();
            foreach (var row in place)
            {
                return row;
            }
            return result;
        }

        /// <summary>
        /// UserNamesByFirstName grabs all of the usernames associated with a first name in the database
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>List<string></returns>
        public List<string> UserNamesByFirstName(string Name)
        {
            string StringBuffer = "";
            List<string> ResultingList = new List<string>();
            List<Customer> MyQuery = context.Customers.Where(row => row.FirstName == Name).ToList();
            foreach (var row in MyQuery)
            {
                //StringBuffer = row.FirstName + " " + row.LastName + ": " + row.UserName;
                ResultingList.Add(row.UserName.ToString());
            }
            return ResultingList;
        }

        /// <summary>
        /// GetUserInfo Returns the Customer associated with a customerID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Customer</returns>
        public Customer GetUserInfo(int userId)
        {
            Customer result = new Customer();
            List<Customer> user = context.Customers.Where(row => row.CustomerId == userId).ToList();
            foreach (var row in user)
            {
                return row;
            }
            return result;
        }

        /// <summary>
        /// GetUserInfo Return the Customer associate with a username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Customer</returns>
        public Customer GetUserInfo(string userName)
        {
            Customer result = new Customer();
            List<Customer> user = context.Customers.Where(row => row.UserName == userName).ToList();
            foreach (var row in user)
            {
                return row;
            }
            return result;
        }

        /// <summary>
        /// ProductFromProductID Return the Product associated with a productID from the database
        /// </summary>
        /// <param name="productID"></param>
        /// <returns>Product</returns>
        public Product ProductFromProductID(int productID)
        {
            Product item = new Product();
            List<Product> result = context.Products.Where(row => row.ProductId == productID).ToList();
            foreach (var row in result)
            {
                item = row;
            }
            return item;
        }

        /// <summary>
        /// InventoryFromProductID return the amount of item assiocated with a productID from the database
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="locationID"></param>
        /// <returns>int</returns>
        public int InventoryFromProductID(int productID, int locationID)
        {
            Inventory item = new Inventory();
            List<Inventory> result = context.Inventories.Where(row => row.ProductId == productID && row.LocationId == locationID).ToList();
            foreach (var row in result)
            {
                item = row;
            }
            return item.Amount;
        }

        /// <summary>
        /// InventoryAtLocation returns the Inventory elements associated with a LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>List<Inventory></returns>
        public List<Inventory> InventoryAtLocation(int locationID)
        {
            List<Inventory> results = context.Inventories.Where(row => row.LocationId == locationID).ToList();
            return results;
        }

        /// <summary>
        /// DetailedInventory Aggregates Information on the products and inventory from a locationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>List<DetailedProductInventory></returns>
        public List<DetailedProductInventory> DetailedInventory(int locationID)
        {
            List<DetailedProductInventory> result = new List<DetailedProductInventory>();
            DetailedProductInventory item;
            List<Inventory> inventory = InventoryAtLocation(locationID);
            foreach (var product in inventory)
            {
                if (product != null)
                {
                    item = new DetailedProductInventory(locationID, product.ProductId);
                    result.Add(item);
                }

            }
            return result;
        }

        /// <summary>
        /// Generates a Location List of all locations in the database
        /// </summary>
        /// <returns>List<Location></returns>
        public List<Location> ListOfLocations()
        {
            List<Location> locations = context.Locations.ToList();
            return locations;
        }

        /// <summary>
        /// Adds a User to the database
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>bool</returns>
        public bool AddUserAccount(Customer newUser)
        {
            try
            {
                context.Add(newUser);
                context.SaveChanges();
            }
            catch
            {
                //logger things?
            }
            return true;
        }

        /// <summary>
        /// Updates a customers default location by locationID
        /// </summary>
        /// <param name="LocationId"></param>
        /// <param name="user"></param>
        /// <returns>Customer</returns>
        public Customer UpdateDefaultLocation(int LocationId, Customer user)
        {
            try 
            {
                List<Customer> DbAccount = context.Customers.Where(row => row.CustomerId == user.CustomerId).ToList();
                foreach( var row in DbAccount)
                {
                    row.DefaultStore = LocationId;
                }
                user.DefaultStore = LocationId;
                context.SaveChanges();
            }
            catch
            {
                //Enter Logger Stuff Here
            }
            return user;
        }

        /// <summary>
        /// Return if a username and password key exists in the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        public Customer IfValidUser(string username, string password)
        {
            Customer result = new Customer();
            List<Customer> list = context.Customers.Where(row => row.UserName == username && row.UserPassword == password).ToList();
            foreach(var row in list)
            {
                result = row;
            }
            return result;
        }

        /// <summary>
        /// Combines the inventory and product details of an inventory list
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>List<DetailedProductInventory></returns>
        public List<DetailedProductInventory> DetailedCart(List<Inventory> cart)
        {
            List<DetailedProductInventory> result = new List<DetailedProductInventory>();
            DetailedProductInventory item;
            foreach (var product in cart)
            {
                if (product != null)
                {
                    item = new DetailedProductInventory(product.LocationId, product.ProductId, product.Amount);
                    result.Add(item);
                }

            }
            return result;
        }

        /// <summary>
        /// Caluculates the total cost of an inventory list
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>double</returns>
        public double TotalCost(List<DetailedProductInventory> cart)
        {
            double cost = 0;
            foreach(var item in cart)
            {
                cost += (item.Product.Price * item.Inventory);
            }

            return cost;
        }

        /// <summary>
        /// Adds an Order to Order table in the database and return the instantiated order
        /// </summary>
        /// <param name="orderToAdd"></param>
        /// <returns>Order</returns>
        public Order AddOrder(Order orderToAdd)
        {
            try
            {
                context.Orders.Add(orderToAdd);
                context.SaveChanges();
            }
            catch
            {
                //Logging?
            };

            
            return context.Orders.First();
        }

        /// <summary>
        /// Update the database to show inventory loss and add order information to database
        /// </summary>
        /// <param name="orderToProcess"></param>
        /// <param name="cart"></param>
        /// <returns>bool</returns>
        public bool ProcessOrder(Order orderToProcess, List<Inventory> cart)
        {

            foreach(var item in cart)
            {
                List<Inventory> LocationIventory = context.Inventories.Where(row => row.LocationId == orderToProcess.LocationId && row.ProductId == item.ProductId).ToList();
                foreach (var stock in LocationIventory)
                {
                    stock.Amount -= item.Amount;
                }

                context.SaveChanges();

                OrderedProduct itemOrdered = new OrderedProduct();
                itemOrdered.OrderId = orderToProcess.OrderId;
                itemOrdered.ProductId = item.ProductId;
                itemOrdered.Amount = item.Amount;

                context.OrderedProducts.Add(itemOrdered);

                context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Retrieve all Orders by Location ID from the database
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>List<Order></returns>
        public List<Order> OrderHistoryByLocation(int locationID)
        {
            List<Order> Orders = context.Orders.Where(row => row.LocationId == locationID).ToList();
            return Orders;
        }

        /// <summary>
        /// Retrieve all Orders by Customer ID from the database
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>List<Order></Order></returns>
        public List<Order> OrderHistoryByUser(int customerID)
        {
            List<Order> Orders = context.Orders.Where(row => row.CustomerId == customerID).ToList();
            return Orders;
        }
    }
}

