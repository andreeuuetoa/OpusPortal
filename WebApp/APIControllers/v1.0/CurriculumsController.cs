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
public class CurriculumsController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public CurriculumsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Curriculums
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Curriculum>>> GetCurriculum()
    {
        if (_context.Curriculum == null)
        {
            return NotFound();
        }
        return await _context.Curriculum.ToListAsync();
    }

    // GET: api/Curriculums/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Curriculum>> GetCurriculum(Guid id)
    {
        if (_context.Curriculum == null)
        {
            return NotFound();
        }
        var curriculum = await _context.Curriculum.FindAsync(id);

        if (curriculum == null)
        {
            return NotFound();
        }

        return curriculum;
    }

    // PUT: api/Curriculums/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="curriculum"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCurriculum(Guid id, Curriculum curriculum)
    {
        if (id != curriculum.Id)
        {
            return BadRequest();
        }

        _context.Entry(curriculum).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CurriculumExists(id))
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

    // POST: api/Curriculums
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="curriculum"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Curriculum>> PostCurriculum(Curriculum curriculum)
    {
        if (_context.Curriculum == null)
        {
            return Problem("Entity set 'AppDbContext.Curriculum'  is null.");
        }
        _context.Curriculum.Add(curriculum);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCurriculum", new { id = curriculum.Id }, curriculum);
    }

    // DELETE: api/Curriculums/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurriculum(Guid id)
    {
        if (_context.Curriculum == null)
        {
            return NotFound();
        }
        var curriculum = await _context.Curriculum.FindAsync(id);
        if (curriculum == null)
        {
            return NotFound();
        }

        _context.Curriculum.Remove(curriculum);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CurriculumExists(Guid id)
    {
        return (_context.Curriculum?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}