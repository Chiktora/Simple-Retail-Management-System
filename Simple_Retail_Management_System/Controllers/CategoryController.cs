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
    public class CategoryController : IEssentialable<Category>
    {
        private ShopContext context;

        public CategoryController()
        {
            this.context = new ShopContext();
        }
        public void Add(Category category)
        {
            var existingCategory = this.context.Categories.Find(category.Id);
            if (existingCategory == null) 
            {
                this.context.Categories.Add(category);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Category is present");
            }

            
        }

        public void Delete(int id)
        {
            var category = this.Get(id);  
            if (category != null)
            {
                this.context.Categories.Remove(category);
                
            }
            else 
            {
                throw new ArgumentException("Category not found");
            }
            
            
        }

        public Category Get(int id)
        {
            var category = context.Categories.FirstOrDefault(x => x.Id == id);
            return category;        
            
        }


        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public void Upgrade(Category category)
        {
            var existingCategory = this.Get(category.Id);
            if (existingCategory != null) 
            {        
                this.context.Entry(existingCategory).CurrentValues.SetValues(category);
                this.context.SaveChanges();
            }
            else 
            {
                throw new ArgumentException("Category not found");
            }
            
        }
    }
}
