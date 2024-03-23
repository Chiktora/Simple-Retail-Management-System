
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
public class ProductionControllerTests
{
    private Mock<ShopContext> _mockContext;
    private Mock<DbSet<Producer>> _mockDbSet;
    private ProducerController _controller;
    private List<Producer> _employeeData;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<ShopContext>();
        _mockDbSet = new Mock<DbSet<Producer>>();
        _controller = new ProducerController(_mockContext.Object);

        _employeeData = new List<Producer>
            {
            new Producer { Id = 1, Name = "Producer A", PhoneNumber = "123-456-7890", Email = "contact@producera.com" },
            new Producer { Id = 2, Name = "Producer B", PhoneNumber = "098-765-4321", Email = "info@producerb.com" }
            };

        _mockDbSet.As<IQueryable<Producer>>().Setup(m => m.Provider).Returns(_employeeData.AsQueryable().Provider);
        _mockDbSet.As<IQueryable<Producer>>().Setup(m => m.Expression).Returns(_employeeData.AsQueryable().Expression);
        _mockDbSet.As<IQueryable<Producer>>().Setup(m => m.ElementType).Returns(_employeeData.AsQueryable().ElementType);
        _mockDbSet.As<IQueryable<Producer>>().Setup(m => m.GetEnumerator()).Returns(() => _employeeData.GetEnumerator());

        _mockDbSet.Setup(m => m.Add(It.IsAny<Producer>())).Callback<Producer>((s) => _employeeData.Add(s));
        _mockDbSet.Setup(m => m.Remove(It.IsAny<Producer>())).Callback<Producer>((s) => _employeeData.Remove(s));
        _mockDbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => _employeeData.FirstOrDefault(d => d.Id == (int)ids[0]));

        _mockContext.Setup(c => c.Producers).Returns(_mockDbSet.Object);

        _controller = new ProducerController(_mockContext.Object);
    }

    [Test]
    public void Add_NewProducer_ShouldAddProducer()
    {
        var newProducer = new Producer { Id = 3, Name = "Producer C", PhoneNumber = "321-654-0987", Email = "support@producerc.com" };
        _controller.Add(newProducer);
        _mockDbSet.Verify(m => m.Add(It.IsAny<Producer>()), Times.Once);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public void Add_ExistingProducer_ShouldThrowArgumentException()
    {
        var existingProducer = new Producer { Id = 1, Name = "Producer A", PhoneNumber = "123-456-7890", Email = "contact@producera.com" };
        _mockDbSet.Setup(m => m.Find(existingProducer.Id)).Returns(existingProducer); // Simulate existing producer

        var ex = Assert.Throws<ArgumentException>(() => _controller.Add(existingProducer));
        Assert.That(ex.Message, Is.EqualTo("Producer is present"), "Expected exception message did not match.");
    }

    [Test]
    public void Delete_ExistingProducer_ShouldRemoveProducer()
    {
        var producerIdToDelete = 1;
        _controller.Delete(producerIdToDelete);
        _mockDbSet.Verify(m => m.Remove(It.IsAny<Producer>()), Times.Once);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }
    [Test]
    public void Get_ExistingProducerId_ShouldReturnCorrectProducer()
    {
        var result = _controller.Get(1);
        ClassicAssert.IsNotNull(result);
        ClassicAssert.AreEqual(1, result.Id);
    }
    [Test]
    public void GetAll_ShouldReturnAllProducers()
    {
        var result = _controller.GetAll();
        ClassicAssert.AreEqual(2, result.Count); // Assuming 2 producers in the setup
    }

    [Test]
    public void Update_NonExistingProducer_ShouldThrowArgumentException()
    {
        // Arrange
        var nonExistingProducer = new Producer { Id = 99, Name = "Non Existing Producer", PhoneNumber = "000-000-0000", Email = "nonexisting@example.com" };

        // Assuming _producerController is an instance of your ProducerController
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _controller.Update(nonExistingProducer));
        Assert.That(ex.Message, Is.EqualTo("Producer not found"), "Expected exception message did not match.");
    }






}




