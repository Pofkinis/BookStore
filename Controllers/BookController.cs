using Book.Models;
using Books.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book.Models.Book>>> Get()
    {
        return Ok(await _service.GetAllBooks());
    }
    
    [HttpGet]
    public async Task<ActionResult<Book.Models.Book>> GetById(int id)
    {
        var book = await _service.GetById(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }
    
    [HttpPost]
    public async Task<ActionResult<Book.Models.Book>> Create(Book.Models.Book book)
    {
        await _service.CreateBook(book);
        
        return CreatedAtAction(nameof(Create), book);
    }
    
    [HttpPut]
    public async Task<ActionResult<Book.Models.Book>> Update(Book.Models.Book book)
    {
        var updatedBook = await _service.UpdateBook(book);
        
        if (updatedBook == null)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete]
    public async Task<ActionResult<Book.Models.Book>> Delete(int id)
    {
        var result = await _service.DeleteBook(id);
        
        if (result)
        {
            return NoContent();
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book.Models.Book>>> GetByAuthorId(int authorId)
    {
        return Ok(await _service.GetByAuthorId(authorId));
    }
}