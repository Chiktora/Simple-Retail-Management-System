using Microsoft.EntityFrameworkCore;
using Simple_Retail_Management_System.Controllers.Interfaces;
using Simple_Retail_Management_System.Data;
using Simple_Retail_Management_System.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Controllers
{
    public class CustomerController : IEssentialable<Customer>
    {
        private ShopContext context;

        public CustomerController()
        {
            context = new ShopContext();
        }
        public CustomerController(ShopContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Adds a new customer to the context
        /// </summary>
        public void Add(Customer item)
        {
            var existingItem = this.context.Customers.Find(item.Id);
            if (existingItem == null)
            {
                this.context.Customers.Add(item);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Customer is present");
            }
        }
        /// <summary>
        /// Deletes a customer from the context by ID
        /// </summary>
        public void Delete(int id)
        {
            var item = this.Get(id);
            this.context.Customers.Remove(item);
            this.context.SaveChanges();
        }
        /// <summary>
        /// Retrieves a customer by ID
        /// </summary>
        public Customer Get(int id)
        {
            var item = context.Customers.FirstOrDefault(x => x.Id == id);
            return item;
        }
        /// <summary>
        /// Retrieves all customers
        /// </summary>
        public List<Customer> GetAll()
        {
            return context.Customers.ToList();
        }
        /// <summary>
        /// Updates an existing customer
        /// </summary>
        public void Update(Customer item)
        {
            var existingItem = this.context.Customers.Find(item.Id);
            if (existingItem != null)
            {
                this.context.Entry(existingItem).CurrentValues.SetValues(item);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Customer not found");
            }
        }
    }
}
