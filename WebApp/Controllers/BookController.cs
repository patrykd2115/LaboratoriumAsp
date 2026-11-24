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
    [HttpGet("Book/Details/{id:int}")]

    public IActionResult Details(int id)
    {
        var book = BookStoreMemory.Books.FirstOrDefault(b => b.Id == id);
        return book is null ? NotFound() : View(book);
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
    
    public async Task<IActionResult> ByYear(int year) { return  View(nameof(Index), BookStoreMemory.Books.Where(b => b.PublicationDate.Value.Year == year)); }

    public async Task<IActionResult> ByIsbn(string isbn)
    {
        var book = BookStoreMemory.Books.FirstOrDefault(b => b.Isbn.Replace("-","") == isbn);
        return book is null ? NotFound():  View(nameof(Details), book);
    }

    public IActionResult Docs(string? path)
    {
        return View(path);
    }
    
}