using Book.Models;
using Books.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.Services;

public class BookService : IBookService
{
    private BookContext _context;

    public BookService(BookContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book.Models.Book>> GetAllBooks()
    {
        return await _context.Books.Include(x => x.Author).ToListAsync();
    }

    public async Task<Book.Models.Book> GetById(int id)
    {
        return await _context.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<IEnumerable<Book.Models.Book>> GetByAuthorId(int authorId)
    {
        return await _context.Books.Include(x => x.Author).Where(x => x.Author.Id == authorId).ToListAsync();
    }

    public async Task<Book.Models.Book> CreateBook(Book.Models.Book book)
    {
        var lastId = _context.Books.Last()?.Id;

        book.Id = ++lastId ?? 1;

        _context.Add(book);
        await _context.SaveChangesAsync();

        return book;
    }

    public async Task<Book.Models.Book> UpdateBook(Book.Models.Book book)
    {
        var bookEntry = await GetById(book.Id);

        if (bookEntry == null)
        {
            return null;
        }

        bookEntry = book;
        _context.Books.Update(bookEntry);
        await _context.SaveChangesAsync();

        return book;
    }

    public async Task<bool> DeleteBook(int id)
    {
        var bookEntry = await GetById(id);
        
        if (bookEntry == null)
        {
            return false;
        }

        _context.Books.Remove(bookEntry);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}