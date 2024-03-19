using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Controllers
{
    public class SalesController : IEssentialable<Sale>
    {
         private ShopContext context;
 public SalesController()
 {
     context = new ShopContext();
 }
 public void Add(Sale item)
 {
     var existingItem = this.context.Sales.Find(item.Id);
     if (existingItem == null)
     {
         this.context.Sales.Add(item);
         this.context.SaveChanges();
     }
     else
     {
         throw new ArgumentException("Sale Already Occured");
     }
 }
 public void Delete(int id)
 {
     var item = this.Get(id);
     if (item != null)
     {
         this.context.Sales.Remove(item);

     }
     else
     {
         throw new ArgumentException("Sale Not Found");
     }
 }
 public Sale Get(int id)
 {
     var item = context.Sales.FirstOrDefault(x => x.Id == id);
     return item;
 }
 public List<Sale> GetAll()
 {
     return context.Sales.ToList();
 }
 public void Upgrade(Sale item)
 {
     var existingItem = this.Get(item.Id);
     if (existingItem != null)
     {
         this.context.Entry(existingItem).CurrentValues.SetValues(item);
         this.context.SaveChanges();
     }
     else
     {
         throw new ArgumentException("Sale Not Found");
     }
 }
    }
}
