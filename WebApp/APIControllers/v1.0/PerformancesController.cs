using Asp.Versioning;
using DAL;
using Domain.Concerts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.APIControllers.v1._0;

/// <summary>
/// 
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PerformancesController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public PerformancesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Performances
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonAtConcert>>> GetPersonAtConcert()
    {
        if (_context.PersonAtConcert == null)
        {
            return NotFound();
        }
        return await _context.PersonAtConcert.ToListAsync();
    }

    // GET: api/Performances/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonAtConcert>> GetPersonAtConcert(Guid id)
    {
        if (_context.PersonAtConcert == null)
        {
            return NotFound();
        }
        var personAtConcert = await _context.PersonAtConcert.FindAsync(id);

        if (personAtConcert == null)
        {
            return NotFound();
        }

        return personAtConcert;
    }

    // PUT: api/Performances/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="personAtConcert"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPersonAtConcert(Guid id, PersonAtConcert personAtConcert)
    {
        if (id != personAtConcert.Id)
        {
            return BadRequest();
        }

        _context.Entry(personAtConcert).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonAtConcertExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Performances
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="personAtConcert"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<PersonAtConcert>> PostPersonAtConcert(PersonAtConcert personAtConcert)
    {
        if (_context.PersonAtConcert == null)
        {
            return Problem("Entity set 'AppDbContext.PersonAtConcert'  is null.");
        }
        _context.PersonAtConcert.Add(personAtConcert);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPersonAtConcert", new { id = personAtConcert.Id }, personAtConcert);
    }

    // DELETE: api/Performances/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonAtConcert(Guid id)
    {
        if (_context.PersonAtConcert == null)
        {
            return NotFound();
        }
        var personAtConcert = await _context.PersonAtConcert.FindAsync(id);
        if (personAtConcert == null)
        {
            return NotFound();
        }

        _context.PersonAtConcert.Remove(personAtConcert);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PersonAtConcertExists(Guid id)
    {
        return (_context.PersonAtConcert?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}