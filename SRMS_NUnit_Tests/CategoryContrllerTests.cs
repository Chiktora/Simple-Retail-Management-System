using Microsoft.EntityFrameworkCore;
using Moq;
using Simple_Retail_Management_System.Controllers;
using Simple_Retail_Management_System.Data.Models;
using Simple_Retail_Management_System.Data;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMS_NUnit_Tests
{
    [TestFixture]
    internal class CategoryContrllerTests
    {
        [TestFixture]
        public class CategoryControllerTests
        {
            private Mock<ShopContext> _mockContext;
            private Mock<DbSet<Category>> _mockDbSet;
            private CategoryController _controller;
            private List<Category> _categoryData;

            [SetUp]
            public void Setup()
            {
                _mockContext = new Mock<ShopContext>();
                _mockDbSet = new Mock<DbSet<Category>>();
                _controller = new CategoryController();
                _categoryData = new List<Category>
                {
                new Category { Id = 1, CategoryName = "Electronics" },
                new Category { Id = 2, CategoryName = "Books" }
                };

                // Setup the mock DbSet
                _mockDbSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(_categoryData.AsQueryable().Provider);
                _mockDbSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(_categoryData.AsQueryable().Expression);
                _mockDbSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(_categoryData.AsQueryable().ElementType);
                _mockDbSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => _categoryData.GetEnumerator());

                // Setup for Add and Remove to manipulate _categoryData
                _mockDbSet.Setup(m => m.Add(It.IsAny<Category>())).Callback<Category>((s) => _categoryData.Add(s));
                _mockDbSet.Setup(m => m.Remove(It.IsAny<Category>())).Callback<Category>((s) => _categoryData.Remove(s));

                // If CategoryController uses Find, setup mock for it
                _mockDbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => _categoryData.FirstOrDefault(d => d.Id == (int)ids[0]));

                // Linking the DbSet with the Mock Context
                _mockContext.Setup(c => c.Categories).Returns(_mockDbSet.Object);

                _controller = new CategoryController(_mockContext.Object);
            }

            [Test]
            public void Add_NewCategory_ShouldAddCategory()
            {
                var newCategory = new Category { Id = 3, CategoryName = "Clothing" };
                _controller.Add(newCategory);
                _mockDbSet.Verify(m => m.Add(It.IsAny<Category>()), Times.Once);
                _mockContext.Verify(m => m.SaveChanges(), Times.Once);
            }

            [Test]
            public void Add_ExistingCustomer_ShouldThrowArgumentException()
            {
                // Arrange
                var existingCategory = new Category { Id = 1, CategoryName = "Electronics" };
                _mockDbSet.Setup(m => m.Find(existingCategory.Id)).Returns(existingCategory); // Simulate that this customer already exists in the DbSet

                // Act & Assert
                var ex = Assert.Throws<ArgumentException>(() => _controller.Add(existingCategory));
                Assert.That(ex.Message, Is.EqualTo("Category is present"), "Expected exception message did not match.");
            }

            [Test]
            public void Delete_ExistingCategory_ShouldRemoveCategory()
            {
                var categoryIdToDelete = 1;
                _controller.Delete(categoryIdToDelete);
                _mockDbSet.Verify(m => m.Remove(It.IsAny<Category>()), Times.Once);
                _mockContext.Verify(m => m.SaveChanges(), Times.Once);
            }

            [Test]
            public void GetAll_ShouldReturnAllCategories()
            {
                var result = _controller.GetAll();
                ClassicAssert.AreEqual(2, result.Count); // Assuming you have 2 categories set up in Setup()
            }

            [Test]
            public void Get_ExistingCategoryId_ShouldReturnCorrectCategory()
            {
                var result = _controller.Get(1);
                ClassicAssert.IsNotNull(result);
                ClassicAssert.AreEqual(1, result.Id);
                ClassicAssert.AreEqual("Electronics", result.CategoryName);
            }

            [Test]
            public void Update_NonExistingCategory_ShouldThrowArgumentException()
            {
                // Arrange
                var nonExistingCategory = new Category { Id = 99, CategoryName = "Non Existing Category" };

                // Assuming _categoryController is an instance of your CategoryController
                // Act & Assert
                var ex = Assert.Throws<ArgumentException>(() => _controller.Update(nonExistingCategory));
                Assert.That(ex.Message, Is.EqualTo("Category not found"), "Expected exception message did not match.");
            }




        }


    }
}
