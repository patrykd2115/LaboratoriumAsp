using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class BookController : Controller
{
    public IActionResult Hello() => Content("Witaj w BookStore!", "text/plain; charset=utf-8");

    public IActionResult Status(int code = 200) => StatusCode(code);

    public IActionResult Details(int id)
    {
        ViewBag.Id = id;
        return View();
    }

    public IActionResult Echo([FromQuery] string name = "nieznajomy", [FromQuery] int count = 1) =>
        Content(string.Join("\n", Enumerable.Repeat($"Cześć, {name}!", Math.Clamp(count, 1, 20))), "text/plain; charset=utf-8");


    public IActionResult Square(int id) => Content($"{id}^2 = {id * id}", "text/plain; charset=utf-8");

    [HttpPost]
    public IActionResult Import([FromBody] BookDto dto) =>
        Json(new { received = dto.Title, dto.Isbn, dto.Price });

    public IActionResult Inspect()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Method: {Request.Method}");
        sb.AppendLine($"Path:   {Request.Path}{Request.QueryString}");
        foreach (var h in Request.Headers) sb.AppendLine($"{h.Key}: {h.Value}");
        return Content(sb.ToString(), "text/plain; charset=utf-8");
    }

    public IActionResult Sum(int a, int b) => Content($"{a} + {b} = {a + b}", "text/plain; charset=utf-8");

    public IActionResult ClientAgent() => Content($"User-Agent: {Request.Headers.UserAgent}", "text/plain; charset=utf-8");

    public IActionResult Health() => Json(new { status = "ok", time = DateTime.Now });
}