﻿using Simple_Retail_Management_System.Controllers.Interfaces;
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
        public void Add(Category item)
        {
            var existingItem = this.context.Categories.Find(item.Id);
            if (existingItem == null) 
            {
                this.context.Categories.Add(item);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Category is present");
            }

            
        }
        public void Delete(int id)
        {
            var item = this.Get(id);  
            if (item != null)
            {
                this.context.Categories.Remove(item);
                
            }
            else 
            {
                throw new ArgumentException("Category not found");
            }
            
            
        }

        public Category Get(int id)
        {
            var item = context.Categories.FirstOrDefault(x => x.Id == id);
            return item;        
            
        }


        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public void Upgrade(Category item)
        {
            var existingItem = this.Get(item.Id);
            if (existingItem != null) 
            {        
                this.context.Entry(existingItem).CurrentValues.SetValues(item);
                this.context.SaveChanges();
            }
            else 
            {
                throw new ArgumentException("Category not found");
            }
            
        }
    }
}