using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SortOrders.API.Models;
using SortOrders.API.Repos;

namespace SortOrders.API.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOrderRepo orderRepo;

    public HomeController(
        ILogger<HomeController> logger,
        IOrderRepo repo
        )
    {
        orderRepo = repo;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View( orderRepo.GetAllOrders().ToList() );
    }

    [HttpPost]
    public IActionResult Index(string district, DateTime from, DateTime to)
    {
        return View( orderRepo.GetOrdersByDistrictAndTime( district, from, to ).ToList() );
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
