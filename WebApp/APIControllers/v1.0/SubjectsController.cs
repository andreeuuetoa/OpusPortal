using Asp.Versioning;
using DAL;
using Domain.Studying_logic;
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
public class SubjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public SubjectsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Subjects
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subject>>> GetSubject()
    {
        if (_context.Subject == null)
        {
            return NotFound();
        }
        return await _context.Subject.ToListAsync();
    }

    // GET: api/Subjects/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Subject>> GetSubject(Guid id)
    {
        if (_context.Subject == null)
        {
            return NotFound();
        }
        var subject = await _context.Subject.FindAsync(id);

        if (subject == null)
        {
            return NotFound();
        }

        return subject;
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubject(Guid id, Subject subject)
    {
        if (id != subject.Id)
        {
            return BadRequest();
        }

        _context.Entry(subject).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubjectExists(id))
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

    // POST: api/Subjects
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Subject>> PostSubject(Subject subject)
    {
        if (_context.Subject == null)
        {
            return Problem("Entity set 'AppDbContext.Subject'  is null.");
        }
        _context.Subject.Add(subject);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSubject", new { id = subject.Id }, subject);
    }

    // DELETE: api/Subjects/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        if (_context.Subject == null)
        {
            return NotFound();
        }
        var subject = await _context.Subject.FindAsync(id);
        if (subject == null)
        {
            return NotFound();
        }

        _context.Subject.Remove(subject);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SubjectExists(Guid id)
    {
        return (_context.Subject?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}