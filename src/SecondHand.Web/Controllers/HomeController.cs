﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SecondHand.Web.Data;
using SecondHand.Web.Models;

namespace SecondHand.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly SecondHandWebContext _context;

    public HomeController(
        ILogger<HomeController> logger,
        SecondHandWebContext context
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
