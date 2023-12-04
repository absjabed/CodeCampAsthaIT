using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodeCampMvc.Models;

namespace CodeCampMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _config;

    public HomeController(ILogger<HomeController> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public IActionResult Index()
    {
            //https://OurSite.com/Home/Index/42
        
        var result = _config.GetValue<string>("IsProduction");
        Console.WriteLine("Is it Production Environment: "+result);

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
