using Moq;
using NUnit.Framework;
using Simple_Retail_Management_System.Data;
using Simple_Retail_Management_System.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Simple_Retail_Management_System.Controllers;

namespace YourProjectName.Tests
{
    [TestFixture]
    public class CategoryControllerTests
    {
        private Mock<DbSet<Category>> mockSet;
        private Mock<ShopContext> mockContext;
        private CategoryController controller;

        [SetUp]
        public void SetUp()
        {
            mockSet = new Mock<DbSet<Category>>();

            var data = new List<Category>
            {
                new Category { CategoryName = "Electronics" },
                new Category { CategoryName = "Books" }
            }.AsQueryable();

            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));

            mockContext = new Mock<ShopContext>();
            mockContext.Setup(c => c.Categories).Returns(mockSet.Object);

            //controller = new CategoryController(mockContext.Object);
        }

        [Test]
        public void Add_NewCategory_ShouldAddCategory()
        {
            // Arrange
            var newCategory = new Category { CategoryName = "Toys" };

            // Act
            controller.Add(newCategory);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Category>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        public void Add_ExistingCategory_ShouldThrowArgumentException()
        {
            // Arrange
            var existingCategory = new Category { CategoryName = "Electronics" };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => controller.Add(existingCategory));
            Assert.That(ex.Message, Is.EqualTo("Category is present"));

            mockSet.Verify(m => m.Add(It.IsAny<Category>()), Times.Never);
            mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }
    }
}