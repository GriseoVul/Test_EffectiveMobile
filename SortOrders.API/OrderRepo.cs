using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using SortOrders.API.Models;

namespace SortOrders.API.Repos;

public class OrderRepo : IOrderRepo
{
    private List<Order> Orders; 
    private readonly ILogger<OrderRepo> _logger;
    private readonly AppSettings _options;
    public OrderRepo(
        IOptions<AppSettings> options,
        ILogger<OrderRepo> logger
    )
    {
        Orders = new List<Order>();
        _options = options.Value;
        _logger = logger;
        LoadFromFile();
    }
    private void LoadFromFile()
    {
        _logger.LogInformation($"Loading orders from file:\n{_options.InputFile}",_options.InputFile);
        try 
        {
            Orders = File.ReadAllLines(_options.InputFile)
                            //.Skip(1)
                            .Select(o => Order.FromCsv( o ))
                            .ToList();
        }
        catch(FormatException formatEx)
        {
            _logger.LogError($"Error loading orders!\n" + formatEx.Message);
        }
        catch(ValidationException validEx)
        {
            _logger.LogError($"Invalid data!:\n" + validEx.Message);
        }
        finally
        {
            Orders.Add(new Order());
        }
    }
    public void SaveToFile(List<Order> orders)
    {
        _logger.LogInformation($"Saving information to {_options.OutputFile}", _options.OutputFile);
        List<string> Data = new();
        foreach(Order o in orders)
        {
            Data.Add(o.ToCsv());
        }
        File.WriteAllLines(_options.OutputFile, Data);
        _logger.LogInformation("Succesfully saved!");
    }
    public IEnumerable<Order> GetAllOrders()
    {
        _logger.LogInformation("Getting all Orders");
        return Orders;
    }
    public IEnumerable<Order> GetOrdersByDistrictAndTime(string district, DateTime from, DateTime to)
    {
        _logger.LogInformation($"Searching orders by {district} from {from.ToString()} to {to.ToString()}");

        return Orders.Where(o => o.District == district)
                     .Where(o => (from < o.DeliveryTime) && (o.DeliveryTime < to) )
                     .ToList();
    }
}
