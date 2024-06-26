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
    public class EmployeeController : IEssentialable<Employee>
    {
        private ShopContext context;

        public EmployeeController()
        {
            this.context = new ShopContext();
        }
        public EmployeeController(ShopContext context) 
        {
            this.context = context;
        }

        /// <summary>
        /// Adds a new employee to the context
        /// </summary>
        public void Add(Employee item)
        {
            var existingItem = this.context.Employees.Find(item.Id);
            if (existingItem == null)
            {
                this.context.Employees.Add(item);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Employee is present");
            }
        }
        /// <summary>
        /// Deletes an employee from the context by ID
        /// </summary
        public void Delete(int id)
        {
            var item = this.Get(id);
            this.context.Employees.Remove(item);
            this.context.SaveChanges();
        }

        public Employee Get(int id)
        {
            var item = context.Employees.FirstOrDefault(x => x.Id == id);
            return item;
        }
        /// <summary>
        /// Retrieves all employees
        /// </summary>
        public List<Employee> GetAll()
        {
            return context.Employees.ToList();
        }
        /// <summary>
        /// Updates an existing employee
        /// </summary>
        public void Update(Employee item)
        {
            var existingItem = this.Get(item.Id);
            if (existingItem != null)
            {
                this.context.Entry(existingItem).CurrentValues.SetValues(item);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Employee not found");
            }
        }
    }
}
