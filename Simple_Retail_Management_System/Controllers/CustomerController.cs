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

        public void Delete(int id)
        {
            var item = this.Get(id);
            if (item != null)
            {
                this.context.Customers.Remove(item);

            }
            else
            {
                throw new ArgumentException("Customer not found");
            }
        }

        public Customer Get(int id)
        {
            var item = context.Customers.FirstOrDefault(x => x.Id == id);
            return item;
        }

        public List<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public void Upgrade(Customer item)
        {
            var existingItem = this.Get(item.Id);
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
