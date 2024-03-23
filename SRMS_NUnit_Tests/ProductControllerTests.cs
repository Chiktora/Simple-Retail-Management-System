using Microsoft.EntityFrameworkCore;
using Moq;
using Simple_Retail_Management_System.Controllers;
using Simple_Retail_Management_System.Data.Models;
using Simple_Retail_Management_System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using NUnit.Framework.Legacy;

namespace SRMS_NUnit_Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<ShopContext> _mockContext;
        private Mock<DbSet<Product>> _mockProductDbSet;
        private Mock<DbSet<Producer>> _mockProducerDbSet;
        private Mock<DbSet<Category>> _mockCategoryDbSet;
        private ProductController _controller;
        private List<Product> _products;
        private List<Producer> _producers;
        private List<Category> _categories;

       [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<ShopContext>();
            _mockProductDbSet = new Mock<DbSet<Product>>();
            _mockProducerDbSet = new Mock<DbSet<Producer>>();
            _mockCategoryDbSet = new Mock<DbSet<Category>>();

            _producers = new List<Producer>
        {
            new Producer { Id = 1, Name = "Producer A", PhoneNumber = "123-456-7890", Email = "contact@producera.com" },
            // Add other producers as needed
        };

            _categories = new List<Category>
        {
            new Category { Id = 1, CategoryName = "Category A" },
            // Add other categories as needed
        };

            _products = new List<Product>
            {
                // Initialize products if needed
            };

            var queryableProducers = _producers.AsQueryable();
            var queryableCategories = _categories.AsQueryable();

            _mockProducerDbSet.As<IQueryable<Producer>>().Setup(m => m.Provider).Returns(queryableProducers.Provider);
            _mockProducerDbSet.As<IQueryable<Producer>>().Setup(m => m.Expression).Returns(queryableProducers.Expression);
            _mockProducerDbSet.As<IQueryable<Producer>>().Setup(m => m.ElementType).Returns(queryableProducers.ElementType);
            _mockProducerDbSet.As<IQueryable<Producer>>().Setup(m => m.GetEnumerator()).Returns(() => queryableProducers.GetEnumerator());

            _mockCategoryDbSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(queryableCategories.Provider);
            _mockCategoryDbSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(queryableCategories.Expression);
            _mockCategoryDbSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(queryableCategories.ElementType);
            _mockCategoryDbSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => queryableCategories.GetEnumerator());

            _mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(_products.AsQueryable().Provider);
            _mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(_products.AsQueryable().Expression);
            _mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(_products.AsQueryable().ElementType);
            _mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(_products.GetEnumerator);


            _mockContext.Setup(c => c.Producers).Returns(_mockProducerDbSet.Object);
            _mockContext.Setup(c => c.Categories).Returns(_mockCategoryDbSet.Object);
            _mockContext.Setup(c => c.Products).Returns(_mockProductDbSet.Object);
            _mockProducerDbSet.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Producer, bool>>>()))
                .Returns<Expression<Func<Producer, bool>>>(predicate => _producers.FirstOrDefault(predicate.Compile()));
            _mockCategoryDbSet.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns<Expression<Func<Category, bool>>>(predicate => _categories.FirstOrDefault(predicate.Compile()));


            _controller = new ProductController(_mockContext.Object);
        }

        //[Test]
        public void AddProduct_ValidData_ShouldAddProductSuccessfully()
        {
            // Arrange
            var newProducer = new Producer { Id = 3, Name = "Producer A", PhoneNumber = "123-456-7890", Email = "contact@producera.com" };
            var newCategory = new Category { Id = 2, CategoryName = "Category A" };
            _producers.Add(newProducer); // Adjusting data to ensure the query matches
            _categories.Add(newCategory); // Adjusting data to ensure the query matches

            var barcode = "123456789";
            var name = "Test Product";
            var stockQuantity = 10;
            var price = 20.99m;
            var producerName = "Producer A";
            var categoryName = "Category A";
            var additionalText = "Test Additional Text";

            // Act
            _controller.AddProduct(barcode, name, stockQuantity, price, producerName, categoryName, additionalText);

            // Assert
            _mockProductDbSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        //[Test]
        public void DeleteProduct_ExistingBarcode_ShouldDeleteProductSuccessfully()
        {
            // Arrange
            var existingProduct = new Product { Barcode = "123456", Name = "Existing Product" };
            _products.Add(existingProduct); // Ensure the product exists in the mocked data set

            // Act
            _controller.DeleteProduct("123456");

            // Assert
            _mockProductDbSet.Verify(m => m.Remove(It.Is<Product>(p => p.Barcode == "123456")), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
       // [Test]
        public void EditProduct_ExistingBarcode_ShouldUpdateProductDetails()
        {
            var existingProduct = new Product { Barcode = "123456789", Producer = new Producer(), Category = new Category() };
            _products.Add(existingProduct);

            string newName = "Updated Product Name";
            int newStockQuantity = 15;
            decimal newPrice = 25.99m;
            string newAdditionalText = "Updated Additional Text";

            _mockProductDbSet.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>())).Returns(existingProduct);

            _controller.EditProduct(existingProduct.Barcode, newName, newStockQuantity, newPrice, "Producer A", "Category A", newAdditionalText);

            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        // [Test]
        public void ReadProduct_ExistingBarcode_ShouldReturnProduct()
        {
            var expectedProduct = new Product { Barcode = "123456789", Name = "Test Product" };
            _products.Add(expectedProduct);

            _mockProductDbSet.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>())).Returns(expectedProduct);

            var result = _controller.ReadProduct(expectedProduct.Barcode);

            ClassicAssert.AreEqual(expectedProduct, result);
        }

       // [Test]
        public void GetProductsByProducer_ValidProducerName_ShouldReturnProducts()
        {
            var producerName = "Producer A";
            var matchingProduct = new Product { Barcode = "123456789", Producer = new Producer { Name = producerName } };
            _products.Add(matchingProduct);

            _mockProductDbSet.Setup(m => m.Where(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_products.Where(p => p.Producer.Name == producerName).AsQueryable());

            var results = _controller.GetProductsByProducer(producerName);

            ClassicAssert.Contains(matchingProduct, results.ToList());
        }

       // [Test]
        public void GetProductsByCategory_ValidCategoryName_ShouldReturnProducts()
        {
            var categoryName = "Category A";
            var matchingProduct = new Product { Barcode = "123456789", Category = new Category { CategoryName = categoryName } };
            _products.Add(matchingProduct);

            _mockProductDbSet.Setup(m => m.Where(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_products.Where(p => p.Category.CategoryName == categoryName).AsQueryable());

            var results = _controller.GetProductsByCategory(categoryName);

            ClassicAssert.Contains(matchingProduct, results.ToList());
        }


    }
}

