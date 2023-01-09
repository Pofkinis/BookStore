using Book.Models;
using Books.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.Services;

public class AuthorService : IAuthorService
{
    private BookContext _context;

    public AuthorService(BookContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author> GetById(int id)
    {
        return await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Author> CreateAuthor(Author author)
    {
        var lastId = _context.Authors.Last()?.Id;

        author.Id = ++lastId ?? 1;

        _context.Add(author);
        await _context.SaveChangesAsync();

        return author;
    }

    public async Task<Author> UpdateAuthor(Author author)
    {
        var authorEntry = await GetById(author.Id);

        if (authorEntry == null)
        {
            return null;
        }

        authorEntry = author;
        _context.Authors.Update(authorEntry);
        await _context.SaveChangesAsync();

        return author;
    }

    public async Task<bool> DeleteAuthor(int id)
    {
        var authorEntry = await GetById(id);
        
        if (authorEntry == null)
        {
            return false;
        }

        _context.Authors.Remove(authorEntry);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}