using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class BookController : Controller
{

    public IActionResult Index()
    {
        return View(BookStoreMemory.Books);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(BookFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        BookStoreMemory.Books.Add(new Book
        {
            Id = BookStoreMemory.Books.Max(b => b.Id) + 1,
            Title = model.Title, Isbn = model.Isbn, PublicationDate = model.PublicationDate,
            NumPages = model.NumPages, Price = model.Price, Author = model.Author
        });
        
        TempData["Success"] = $"Dodano książkę „{model.Title}”.";   // przetrwa jedno przekierowanie
        return RedirectToAction(nameof(Index));                   // PRG: F5 nie wyśle formularza drugi raz
    }
}