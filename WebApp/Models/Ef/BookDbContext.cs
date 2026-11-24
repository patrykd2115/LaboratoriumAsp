using Microsoft.EntityFrameworkCore;

namespace WebApp.Models.Ef;

public class BookDbContext : DbContext
{
    
    public DbSet<Book> Books { get; set; }
    
    public BookDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder b)
    {
        //base.OnModelCreating(modelBuilder);
        b.Entity<Book>(e =>
        {
            e.Property(x => x.Price).HasPrecision(10, 2);
            e.HasIndex(x => x.Isbn).IsUnique();
        });
        b.Entity<Book>().HasData(                                    // dane startowe (część C)
            new Book { Id = 1, Title = "Solaris", Isbn = "978-83-08-06534-1", PublicationDate = new(1961, 1, 1), NumPages = 204, Price = 39.90m, Author = "Stanisław Lem" },
            new Book { Id = 2, Title = "Lalka",   Isbn = "978-83-7327-000-8", PublicationDate = new(1890, 1, 1), NumPages = 720, Price = 29.50m, Author = "Bolesław Prus" });
        
    }
    
}