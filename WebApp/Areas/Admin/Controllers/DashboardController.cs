using Microsoft.AspNetCore.Mvc;

namespace WebApp.Area.Admin.Controllers;

[Area("Admin")]

public class DashboardController: Controller
{
    public IActionResult Index()
    {
        return View();
    }
}