using Simple_Retail_Management_System.Data.Models;
using Simple_Retail_Management_System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Retail_Management_System.Controllers.Interfaces
{
    internal interface IEssentialable<T>
    {
        // Get all records
        public List<T> GetAll();
        
        // Get record by id
        public T Get(int id);

        // Add item in table
        public void Add(T item);

        // Update item in table
        public void Update(T item);

        // Delete item by id
        public void Delete(int id);

    }
}
