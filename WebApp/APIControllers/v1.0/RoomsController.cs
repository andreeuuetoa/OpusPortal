using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain.Studying_logic;

namespace WebApp.APIControllers.v1._0;

/// <summary>
/// 
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public RoomsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Rooms
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> GetRoom()
    {
        if (_context.Room == null)
        {
            return NotFound();
        }
        return await _context.Room.ToListAsync();
    }

    // GET: api/Rooms/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> GetRoom(Guid id)
    {
        if (_context.Room == null)
        {
            return NotFound();
        }
        var room = await _context.Room.FindAsync(id);

        if (room == null)
        {
            return NotFound();
        }

        return room;
    }

    // PUT: api/Rooms/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="room"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoom(Guid id, Room room)
    {
        if (id != room.Id)
        {
            return BadRequest();
        }

        _context.Entry(room).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoomExists(id))
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

    // POST: api/Rooms
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Room>> PostRoom(Room room)
    {
        if (_context.Room == null)
        {
            return Problem("Entity set 'AppDbContext.Room'  is null.");
        }
        _context.Room.Add(room);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRoom", new { id = room.Id }, room);
    }

    // DELETE: api/Rooms/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(Guid id)
    {
        if (_context.Room == null)
        {
            return NotFound();
        }
        var room = await _context.Room.FindAsync(id);
        if (room == null)
        {
            return NotFound();
        }

        _context.Room.Remove(room);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RoomExists(Guid id)
    {
        return (_context.Room?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}