using App.BLL.Contracts;
using App.DAL.Contracts;
using Asp.Versioning;
using AutoMapper;
using Base.Helpers;
using BLL.DTO.Library;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers.Library;

namespace WebApp.APIControllers.v1._0;

/// <summary>
/// Specify the actions of the MUBA library.
/// For students and teachers - get the books lented out for them.
/// For administrators - lend books out.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BooksLentOutController : ControllerBase
{
    private readonly IAppUOW _uow;
    private readonly IAppBLL _bll;
    private readonly BookLentOutMapper _mapper;

    /// <summary>
    /// Create an instance of the BooksLentOutController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="bll"></param>
    /// <param name="autoMapper"></param>
    public BooksLentOutController(IAppUOW uow, IAppBLL bll, IMapper autoMapper)
    {
        _uow = uow;
        _bll = bll;
        _mapper = new BookLentOutMapper(autoMapper);
    }

    // GET: api/BooksLentOut
    /// <summary>
    /// Get all the books that have been lent out.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookLentOut>>> GetBooksLentOut()
    {
        var booksLentOut = await _uow.BookLentOutRepository.All();

        return Ok(booksLentOut);
    }
    
    // GET: api/BooksLentOut/5
    /// <summary>
    /// Get books lent out by user with the specified ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<BookLentOut>>> GetBooksLentOut(Guid id)
    {
        var bookLentOut = await _uow.BookLentOutRepository.AllWithUserId(id);

        return Ok(bookLentOut);
    }

    // PUT: api/BooksLentOut/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Modify the parameters of the book lent out, e.g. the deadline.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="bookLentOut"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutBookLentOut(Guid id, BookLentOut bookLentOut)
    {
        if (id != bookLentOut.Id)
        {
            return BadRequest();
        }

        var updatedBookLentOut = await _uow.BookLentOutRepository.UpdateById(id);

        if (updatedBookLentOut == null)
        {
            return BadRequest();
        }

        return CreatedAtAction("GetBooksLentOut", new { id }, bookLentOut);
    }

    // POST: api/BooksLentOut
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Lend out a book.
    /// </summary>
    /// <param name="bookLentOut"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<BookLentOut>> PostBookLentOut(BookLentOut bookLentOut)
    {
        bookLentOut.LentAt = DateTime.UtcNow;
        bookLentOut.ReturnAt = DateTime.UtcNow.AddDays(28);
        
        var addedBook = await _bll.BookLentOutService.Add(bookLentOut);

        if (addedBook == null)
        {
            return BadRequest();
        }

        return CreatedAtAction("GetBooksLentOut", new { id = bookLentOut.Id }, bookLentOut);
    }

    // DELETE: api/BooksLentOut/5
    /// <summary>
    /// Delete a lent out book - as if the book has been returned to the library.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBookLentOut(Guid id)
    {
        var returnedBookLentOut = await _uow.BookLentOutRepository.Find(id);

        if (returnedBookLentOut == null)
        {
            return BadRequest();
        }
        
        returnedBookLentOut.ReturnedAt = DateTime.UtcNow;

        await _uow.BookLentOutRepository.Update(returnedBookLentOut);
        
        return CreatedAtAction("GetBooksLentOut", new { id }, returnedBookLentOut);
    }
}