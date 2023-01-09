using Book.Models;
using Books.Services;
using Books.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorController(IAuthorService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> Get()
    {
        return Ok(await _service.GetAllAuthors());
    }
    
    [HttpGet]
    public async Task<ActionResult<Author>> GetById(int id)
    {
        var author = await _service.GetById(id);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }
    
    [HttpPost]
    public async Task<ActionResult<Author>> Create(Author author)
    {
        await _service.CreateAuthor(author);
        
        return CreatedAtAction(nameof(Create), author);
    }
    
    [HttpPut]
    public async Task<ActionResult<Author>> Update(Author author)
    {
        var updatedAuthor = await _service.UpdateAuthor(author);
        
        if (updatedAuthor == null)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete]
    public async Task<ActionResult<Author>> Delete(int id)
    {
        var result = await _service.DeleteAuthor(id);
        
        if (result)
        {
            return NoContent();
        }

        return BadRequest();
    }
}