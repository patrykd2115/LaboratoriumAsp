using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class BookController(IBookService service) : Controller
{

    public async Task<IActionResult> Index() => View(await service.GetPageAsync(1, 10, null));

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpGet("Book/Details/{id:int}")]

    public async Task<IActionResult> Details(int id)
    {
        var book = await service.GetByIdAsync(id);
        return book is null ? NotFound() : View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var result = await service.AddAsync(new Book
        {
            Id = model.Id,Title = model.Title, Isbn = model.Isbn, PublicationDate = model.PublicationDate,
            NumPages = model.NumPages, Price = model.Price, Author = model.Author
        });
        
        TempData["Success"] = $"Dodano książkę „{model.Title}” pod id {result.Id}";   // przetrwa jedno przekierowanie
        return RedirectToAction(nameof(Index));                   // PRG: F5 nie wyśle formularza drugi raz
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = service.GetByIdAsync(id).GetAwaiter().GetResult();
        if(book is null) return NotFound();
        return View(new BookEditViewModel()
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.Isbn,
            PublicationDate = book.PublicationDate,
            NumPages = book.NumPages,
            Price = book.Price,
            Author = book.Author
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BookEditViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var result = await service.UpdateAsync(new Book
        {
            Title = vm.Title, Isbn = vm.Isbn, PublicationDate = vm.PublicationDate,
            NumPages = vm.NumPages, Price = vm.Price, Author = vm.Author
        });
        
        TempData["Success"] = $"Dodano książkę „{vm.Title}” pod id {vm.Id}";   // przetrwa jedno przekierowanie
        return RedirectToAction(nameof(Index));  
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await service.GetByIdAsync(id);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(BookDeleteViewModel vm)
    {
        var result = await service.DeleteAsync(vm.Id);
        if (result)
        {
            TempData["Success"] = $"Usunięto książkę o id {vm.Id}.";
        }
        else
        {
            TempData["Failed"] = $"Nie usunięto książki";
        }
        return RedirectToAction(nameof(Index));
    }
}