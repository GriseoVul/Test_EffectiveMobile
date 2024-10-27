using System;
using SortOrders.API.Models;

namespace SortOrders.API.Repos;

public interface IOrderRepo
{
    IEnumerable<Order> GetAllOrders();
    IEnumerable<Order> GetOrdersByDistrictAndTime(string district, DateTime from, DateTime to);
    void SaveToFile(List<Order> orders);
}
