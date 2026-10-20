using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Hello()
    {
        return Content("Hello from ASP.NET", contentType:"text/plain; charset=utf-8");
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Age(DateTime Birth)
    {
        DateTime now = DateTime.Now;

        int age = now.Year - Birth.Year;

        if (Birth.Month > now.Month ||
            (Birth.Month == now.Month && Birth.Day > now.Day))
        {
            age--;
        }

        ViewBag.birthday = age;

        return View();
    }

    public IActionResult AgeForm()
    {
        return View();
    }

}