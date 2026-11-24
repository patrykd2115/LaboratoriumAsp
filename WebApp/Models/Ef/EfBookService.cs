using Microsoft.EntityFrameworkCore;

namespace WebApp.Models.Ef;

public class EfBookService(BookDbContext context) : IBookService
{
    public async Task<PagedResult<Book>> GetPageAsync(int page, int pageSize, string? search = null)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);
        IQueryable<Book> q = context.Books.AsNoTracking();                 // odczyt bez śledzenia zmian
        if (!string.IsNullOrWhiteSpace(search)) q = q.Where(b => b.Title.Contains(search));
        var total = await q.CountAsync();
        var items = await q.OrderBy(b => b.Title)
            .Skip((page - 1) * pageSize).Take(pageSize)  // stronicowanie
            .ToListAsync();
        return new PagedResult<Book>(items, page, pageSize, total);
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        var book = await context.Books.FindAsync(id);
        return book;
    }

    public async Task<Book> AddAsync(Book book)
    {
        var result = await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public Task<bool> UpdateAsync(Book book)
    {
        var result = context.Books.Update(book);
        context.SaveChanges();
        return Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await context.Books.FindAsync(id);
        if (book is null) return false;
        var r = context.Books.Remove(book);
        await context.SaveChangesAsync();
        return true;
    }
}