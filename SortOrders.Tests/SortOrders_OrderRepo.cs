using System;
using System.ComponentModel.DataAnnotations;
using SortOrders.API.Models;
using Xunit;
namespace SortOrders.Tests;

public class SortOrders_Orde
{
    [Fact]
    public void Test1_CreationDefaults()
    {
        var Order = new Order();
        bool result = true;
        result = Order.Id == 0 || Order.Weight == 0 || Order.District == String.Empty || Order.DeliveryTime == DateTime.MinValue; 
        
        Assert.True(result, "Model should be empty");
    }
    [Fact]
    public void Test2_Valid_values()
    {
        var Order = new Order
        {
            Id = 2,
            Weight = 2.4,
            District = "testDistrict",
            DeliveryTime = DateTime.Now
        };
        var exc = Record.Exception(() => Order.Validate());
        Assert.Null(exc);
    }
    [Fact]
    public void Test3_Invalid_value_Weight()
    {
        var Order = new Order
        {
            Id = 2,
            Weight = -2.4,
            District = "testDistrict",
            DeliveryTime = DateTime.Now
        };
        
        Assert.Throws<ValidationException>(() => Order.Validate());
    }
    [Fact]
    public void Test4_ValidCSV()
    {
        var Order = new Order();
        var exc = Record.Exception( () => Order.FromCsv("1,4.5,TestDistrict,10.10.2024 12:20:00") );
        Assert.Null(exc);
    }
    [Fact]
    public void Test5_InvalidCSV()
    {
        var Order = new Order();
        Assert.Throws<FormatException>(() =>  Order.FromCsv(""));
    }
}
