using Asp.Versioning;
using DAL;
using Domain.Contacts;
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
public class ContactsController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public ContactsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Contacts
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
    {
        if (_context.Contact == null)
        {
            return NotFound();
        }
        return await _context.Contact.ToListAsync();
    }

    // GET: api/Contacts/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetContact(Guid id)
    {
        if (_context.Contact == null)
        {
            return NotFound();
        }
        var contact = await _context.Contact.FindAsync(id);

        if (contact == null)
        {
            return NotFound();
        }

        return contact;
    }

    // PUT: api/Contacts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="contact"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutContact(Guid id, Contact contact)
    {
        if (id != contact.Id)
        {
            return BadRequest();
        }

        _context.Entry(contact).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContactExists(id))
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

    // POST: api/Contacts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Contact>> PostContact(Contact contact)
    {
        if (_context.Contact == null)
        {
            return Problem("Entity set 'AppDbContext.Contact'  is null.");
        }
        _context.Contact.Add(contact);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
    }

    // DELETE: api/Contacts/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(Guid id)
    {
        if (_context.Contact == null)
        {
            return NotFound();
        }
        var contact = await _context.Contact.FindAsync(id);
        if (contact == null)
        {
            return NotFound();
        }

        _context.Contact.Remove(contact);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ContactExists(Guid id)
    {
        return (_context.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}