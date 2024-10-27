using System;
using System.ComponentModel.DataAnnotations;
using SortOrders.API.Models;
using SortOrders.API.Repos;
using Xunit;
namespace SortOrders.Tests;

public class SortOrders_OrderRepo
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
        bool result = true;
        var Order = new Order
        {
            Id = 2,
            Weight = 2.4,
            District = "testDistrict",
            DeliveryTime = DateTime.Now
        };
        try
        {
            Order.Validate();
        }
        catch(Exception)
        {
            result = false;
        }
        Assert.True(result, "Valid values");
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
}
