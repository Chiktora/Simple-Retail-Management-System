
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Simple_Retail_Management_System.Controllers;
using Simple_Retail_Management_System.Data;
using Simple_Retail_Management_System.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<ShopContext> _mockContext;
        private Mock<DbSet<Employee>> _mockDbSet;
        private EmployeeController _controller;
        private List<Employee> _employeeData;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<ShopContext>();
            _mockDbSet = new Mock<DbSet<Employee>>();
            _controller = new EmployeeController(_mockContext.Object);

            _employeeData = new List<Employee>
            {
            new Employee { Id = 1, Name = "Alice Smith", PhoneNumber = "123-456-7890", Email = "alice@example.com", Position = "Manager"},
            new Employee { Id = 2, Name = "Bob Johnson", PhoneNumber = "098-765-4321", Email = "bob@example.com", Position = "Assistant"}
            };

            _mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(_employeeData.AsQueryable().Provider);
            _mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(_employeeData.AsQueryable().Expression);
            _mockDbSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(_employeeData.AsQueryable().ElementType);
            _mockDbSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(() => _employeeData.GetEnumerator());

            _mockDbSet.Setup(m => m.Add(It.IsAny<Employee>())).Callback<Employee>((s) => _employeeData.Add(s));
            _mockDbSet.Setup(m => m.Remove(It.IsAny<Employee>())).Callback<Employee>((s) => _employeeData.Remove(s));
            _mockDbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => _employeeData.FirstOrDefault(d => d.Id == (int)ids[0]));

            _mockContext.Setup(c => c.Employees).Returns(_mockDbSet.Object);

            _controller = new EmployeeController(_mockContext.Object);

        }

    [Test]
    public void Add_NewEmployee_ShouldAddEmployee()
    {
        var newEmployee = new Employee { Id = 3, Name = "Charlie Davis", PhoneNumber = "321-654-0987", Email = "charlie@example.com", Position = "Developer", HireDate = DateTime.Now };
        _controller.Add(newEmployee);
        _mockDbSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }
    [Test]
    public void Add_ExistingEmployee_ShouldThrowArgumentException()
    {
        // Arrange
        var existingEmployee = new Employee { Id = 1, Name = "John Doe", PhoneNumber = "123-456-7890", Email = "johndoe@example.com", Position = "Developer"};
        _mockDbSet.Setup(m => m.Find(existingEmployee.Id)).Returns(existingEmployee); // Simulate that this employee already exists in the DbSet

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _controller.Add(existingEmployee));

        Assert.That(ex.Message, Is.EqualTo("Employee is present"), "Expected exception message did not match.");
    }

    [Test]
    public void Delete_ExistingEmployee_ShouldRemoveEmployee()
    {
        var employeeIdToDelete = 1;
        _controller.Delete(employeeIdToDelete);
        _mockDbSet.Verify(m => m.Remove(It.IsAny<Employee>()), Times.Once);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }
    [Test]
    public void Get_ExistingEmployeeId_ShouldReturnCorrectEmployee()
    {
        var result = _controller.Get(1);
        ClassicAssert.IsNotNull(result);
        ClassicAssert.AreEqual(1, result.Id);
    }
    [Test]
    public void GetAll_ShouldReturnAllEmployees()
    {
        var result = _controller.GetAll();
        ClassicAssert.AreEqual(2, result.Count); // Assuming 2 employees are set up in Setup()
    }


    [Test]
    public void Update_NonExistingEmployee_ShouldThrowArgumentException()
    {
        // Arrange
        var nonExistingEmployee = new Employee { Id = 99, Name = "Non Existing Employee", PhoneNumber = "000-000-0000", Email = "nonexisting@example.com", Position = "Nonexistent Position", HireDate = DateTime.Now };

        // Assuming _employeeController is an instance of your EmployeeController
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _controller.Update(nonExistingEmployee));
        Assert.That(ex.Message, Is.EqualTo("Employee not found"), "Expected exception message did not match.");
    }







}



