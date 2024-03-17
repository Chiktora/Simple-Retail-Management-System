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
    public class ProducerController : IEssentialable<Producer>
    {
        private ShopContext context;

        public ProducerController()
        {
            this.context = new ShopContext();
        }
        public void Add(Producer item)
        {
            var existingItem = this.context.Producers.Find(item.Id);
            if (existingItem == null)
            {
                this.context.Producers.Add(item);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Producer is present");
            }
        }

        public void Delete(int id)
        {
            var item = this.Get(id);
            if (item != null)
            {
                this.context.Producers.Remove(item);

            }
            else
            {
                throw new ArgumentException("Producer not found");
            }
        }

        public Producer Get(int id)
        {
            var item = context.Producers.FirstOrDefault(x => x.Id == id);
            return item;
        }

        public List<Producer> GetAll()
        {
            return context.Producers.ToList();
        }

        public void Upgrade(Producer item)
        {
            var existingItem = this.Get(item.Id);
            if (existingItem != null)
            {
                this.context.Entry(existingItem).CurrentValues.SetValues(item);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Producer not found");
            }
        }
    }
}
