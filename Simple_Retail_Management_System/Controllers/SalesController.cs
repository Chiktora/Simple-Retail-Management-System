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
    public class SalesController
    {
         private ShopContext context;
             public SalesController()
             {
                 context = new ShopContext();
             }
        public SalesController(ShopContext context)
        {
            context = new ShopContext();
        }


        public void DeleteSale(int saleId)
        {
            var sale = context.Sales.Include(s => s.OrderDetails).FirstOrDefault(s => s.Id == saleId);
            if (sale != null)
            {              
                context.OrderDetails.RemoveRange(sale.OrderDetails);      
                context.Sales.Remove(sale);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Sale Not Found");
            }
        }

        public void AddSaleWithProductQuantities(int employeeId, int customerId, DateTime saleDate, Dictionary<string, int> productQuantities)
        {
            // Create a new sale object
            var sale = new Sale
            {
                EmployeeId = employeeId,
                CustomerId = customerId,
                SaleDate = saleDate,
                SalesPrice = 0 
            };

            
            context.Sales.Add(sale);

           
            foreach (var entry in productQuantities)
            {
                string barcode = entry.Key;
                int quantity = entry.Value;

                // Find the product by its barcode
                var product = context.Products.SingleOrDefault(p => p.Barcode == barcode);
                if (product == null)
                {
                    
                    throw new ArgumentException($"No product with barcode {barcode} was found.");
                }

                // Calculate total price for given quantity
                var productPrice = product.Price * quantity;


                var orderDetail = new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = quantity,
                    Sale = sale, 
                };

                
                context.OrderDetails.Add(orderDetail);

                // Add to the total sales price
                sale.SalesPrice += productPrice;
            }

            // Save changes to the database
            context.SaveChanges();
        }

        public class SaleDetailsDto
        {
            public int SaleId { get; set; }
            public string EmployeeName { get; set; }
            public DateTime SaleDate { get; set; }
            public List<ProductDetailDto> SoldProducts { get; set; } = new List<ProductDetailDto>();
            public decimal TotalPrice { get; set; }
            public CustomerDto Customer { get; set; }
        }

        public class ProductDetailDto
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
        }

        public class CustomerDto
        {
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
        }

        public SaleDetailsDto GetSaleDetails(int saleId)
        {
            var sale = context.Sales
                .Where(s => s.Id == saleId)
                .Include(s => s.Employee)
                .Include(s => s.Customer)
                .Include(s => s.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Select(s => new SaleDetailsDto
                {
                    SaleId = s.Id,
                    EmployeeName = s.Employee.Name,
                    SaleDate = s.SaleDate,
                    SoldProducts = s.OrderDetails.Select(od => new ProductDetailDto
                    {
                        ProductName = od.Product.Name,
                        Quantity = od.Quantity
                    }).ToList(),
                    TotalPrice = s.SalesPrice,
                    Customer = s.Customer != null ? new CustomerDto
                    {
                        Name = s.Customer.Name,
                        PhoneNumber = s.Customer.PhoneNumber,
                        Email = s.Customer.Email
                    } : null
                })
                .FirstOrDefault();

            if (sale == null)
            {
                throw new Exception($"Sale with ID {saleId} not found.");
            }

            return sale;
        }


    }
}
