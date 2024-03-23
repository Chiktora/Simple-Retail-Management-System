using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using Simple_Retail_Management_System.Controllers;
using Simple_Retail_Management_System.Data;
using Simple_Retail_Management_System.Data.Models;
using NUnit.Framework.Legacy;
using System;
using System.Linq;

[TestFixture]
public class CustomerControllerTests
{
    private Mock<ShopContext> _mockContext;
    private Mock<DbSet<Customer>> _mockDbSet;
    private CustomerController _controller;
    private List<Customer> _customerData;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<ShopContext>();
        _mockDbSet = new Mock<DbSet<Customer>>();
        _controller = new CustomerController(_mockContext.Object);
        _customerData = new List<Customer>
        {
            new Customer {
                Id = 1,
                Name = "John Doe",
                PhoneNumber = "123-456-7890",
                Email = "johndoe@example.com",
            },
            new Customer {
                Id = 2,
                Name = "Jane Doe",
                PhoneNumber = "098-765-4321",
                Email = "janedoe@example.com",
            }
        };

        // Setting up the mock DbSet
        _mockDbSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(_customerData.AsQueryable().Provider);
        _mockDbSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(_customerData.AsQueryable().Expression);
        _mockDbSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(_customerData.AsQueryable().ElementType);
        _mockDbSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(_customerData.AsQueryable().GetEnumerator());

        _mockDbSet.Setup(m => m.Add(It.IsAny<Customer>())).Callback<Customer>((s) => _customerData.Add(s));
        _mockDbSet.Setup(m => m.Remove(It.IsAny<Customer>())).Callback<Customer>((s) => _customerData.Remove(s));
        _mockDbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => _customerData.FirstOrDefault(d => d.Id == (int)ids[0]));


        // Linking the DbSet with the Mock Context
        _mockContext.Setup(c => c.Customers).Returns(_mockDbSet.Object);


        _controller = new CustomerController(_mockContext.Object);
    }

    [Test]
    public void Add_NewCustomer_ShouldAddCustomer()
    {
        var newCustomer = new Customer { Id = 3, Name = "Sam Smith", PhoneNumber = "123-456-7890", Email = "SamSmith@example.com" };
        _controller.Add(newCustomer);
        _mockDbSet.Verify(db => db.Add(It.IsAny<Customer>()), Times.Once);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public void Add_ExistingCustomer_ShouldThrowArgumentException()
    {
        // Arrange
        var existingCustomer = new Customer { Id = 1, Name = "John Doe", PhoneNumber = "123-456-7890",Email = "johndoe@example.com" };
        _mockDbSet.Setup(m => m.Find(existingCustomer.Id)).Returns(existingCustomer); // Simulate that this customer already exists in the DbSet

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _controller.Add(existingCustomer));
        Assert.That(ex.Message, Is.EqualTo("Customer is present"), "Expected exception message did not match.");
    }


    [Test]
    public void Delete_ExistingCustomer_ShouldRemoveCustomer()
    {
        _controller.Delete(1);
        _mockDbSet.Verify(db => db.Remove(It.IsAny<Customer>()), Times.Once);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }



    [Test]
    public void Update_NonExistingCustomer_ShouldThrowArgumentException()
    {
        // Arrange
        var nonExistingCustomer = new Customer { Id = 99, Name = "Non Existing", PhoneNumber = "000-000-0000", Email = "nonexisting@example.com" };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _controller.Update(nonExistingCustomer));
        Assert.That(ex.Message, Is.EqualTo("Customer not found"), "Expected exception message did not match.");
    }


    [Test]
    public void Get_ExistingCustomerId_ShouldReturnCustomer()
    {
        var result = _controller.Get(1);
        ClassicAssert.IsNotNull(result);
        ClassicAssert.AreEqual(1, result.Id);
    }

    [Test]
    public void GetAll_ShouldReturnAllCustomers()
    {
        var result = _controller.GetAll();
        ClassicAssert.AreEqual(2, result.Count); // Assuming you have 2 customers set up in Setup()
    }






}



