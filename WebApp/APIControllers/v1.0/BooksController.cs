using App.BLL.Contracts;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers.Library;
using Public.DTO.v1._0.Library;

namespace WebApp.APIControllers.v1._0;

/// <summary>
/// Get, add, modify or delete the books in the MUBA library.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Admin")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BooksController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly BookMapper _mapper;

    /// <summary>
    /// Create an instance of BooksController.
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="autoMapper"></param>
    public BooksController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new BookMapper(autoMapper);
    }

    // GET: api/Books
    /// <summary>
    /// Get all the books registered in the library.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _bll.BookService.All();

        var res = books
            .Select(b => _mapper.Map(b))
            .ToList();

        return Ok(res);
    }

    // GET: api/Books/5
    /// <summary>
    /// Get the book with the specified ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(Guid id)
    {
        var book = await _bll.BookService.Find(id);

        if (book == null)
        {
            return NotFound();
        }

        var res = _mapper.Map(book);

        return Ok(res);
    }

    // PUT: api/Books/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Modify the book with the specified ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="book"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(Guid id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        var BLLBook = _mapper.Map(book);

        if (BLLBook == null)
        {
            return BadRequest();
        }

        var updatedBook = await _bll.BookService.Update(BLLBook);

        if (updatedBook == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetBooks), new
        {
            Version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = book.Id
        }, book);
    }

    // POST: api/Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a book.
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        var BLLBook = _mapper.Map(book);

        if (BLLBook == null)
        {
            return BadRequest();
        }
        
        var addedBook = await _bll.BookService.Add(BLLBook);

        if (addedBook == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetBooks), new {
            Version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = book.Id
        }, addedBook);
    }

    // DELETE: api/Books/5
    /// <summary>
    /// Delete a book.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var removedBook = await _bll.BookService.RemoveById(id);

        if (removedBook == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetBooks), new {
            Version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id
        }, removedBook);
    }
}