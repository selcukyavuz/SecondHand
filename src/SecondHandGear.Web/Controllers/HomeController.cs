using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SecondHandGear.Web.Data;
using SecondHandGear.Web.Models;

namespace SecondHandGear.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly SecondHandGearWebContext _context;

    public HomeController(
        ILogger<HomeController> logger,
        SecondHandGearWebContext context
        )
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
