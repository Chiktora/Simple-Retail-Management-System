using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Controllers
{
    public class ProductController : IEssentialable<Product>
    {
        private ShopContext context;

        public ProductController()
        {
            this.context = new ShopContext();
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();      
        }

        public Product Get(int id)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        public void Add(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

        public void Upgrade(Product product)
        {
            var productItem = this.Get(product.Id);
            this.context.Entry(productItem).CurrentValues.SetValues(product);
            this.context.SaveChanges();
        }

        public void Delete(int id)
    {
    }
}
