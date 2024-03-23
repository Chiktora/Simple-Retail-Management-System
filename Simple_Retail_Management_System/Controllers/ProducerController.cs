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
        public ProducerController(ShopContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Adds a new producer to the context
        /// </summary>
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

        /// <summary>
        /// Deletes a producer from the context by ID
        /// </summary>
        public void Delete(int id)
        {
            var item = this.Get(id);
            this.context.Producers.Remove(item);
            this.context.SaveChanges();
        }
        /// <summary>
        /// Retrieves a producer by ID
        /// </summary>
        public Producer Get(int id)
        {
            var item = context.Producers.FirstOrDefault(x => x.Id == id);
            return item;
        }
        /// <summary>
        /// Retrieves all producers
        /// </summary>
        public List<Producer> GetAll()
        {
            return context.Producers.ToList();
        }
        /// <summary>
        /// Updates an existing producer
        /// </summary>
        public void Update(Producer item)
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
