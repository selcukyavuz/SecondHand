using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StravaStore.Data;
using StravaStore.Models;

namespace StravaStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly StravaStoreContext _context;

    public HomeController(
        ILogger<HomeController> logger,
        StravaStoreContext context
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
