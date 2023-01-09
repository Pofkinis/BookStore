using Book.Models;

namespace Books.Seeders;

public class DataSeeder
{
    public static async void SeedData(BookContext context)
    {
        var author1 = new Author(1, "Jonas", "Jonaitis");
        var author2 = new Author(2, "Petras", "Petras");
        if (!context.Authors.Any())
        {
            context.Authors.Add(author1);
            context.Authors.Add(author2);
            await context.SaveChangesAsync();
        }
        
        if (!context.Books.Any())
        {
            context.Books.Add(new Book.Models.Book(1, author1, "Title 1"));
            context.Books.Add(new Book.Models.Book(2, author2, "Title 2"));
            await context.SaveChangesAsync();
        }
    }
}