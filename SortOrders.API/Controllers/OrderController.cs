using System;
using Microsoft.AspNetCore.Mvc;
using SortOrders.API.Models;
using SortOrders.API.Repos;

namespace SortOrders.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepo OrderRepo;
    private readonly ILogger<OrderController> _logger;
    public OrderController(
        IOrderRepo orderRepo,
        ILogger<OrderController> logger
    )
    {
        OrderRepo = orderRepo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await Task.Run(()=> OrderRepo.GetAllOrders());
        
        return Ok(result);
    }
    [HttpGet("search")]
    public async Task<IActionResult> Search(string district, DateTime from, DateTime to)
    {
        var result = await Task.Run(()=> OrderRepo.GetOrdersByDistrictAndTime(district, from, to));
        await Task.Run(()=> OrderRepo.SaveToFile( result.ToList() ));
        return Ok(result);
    }
}
