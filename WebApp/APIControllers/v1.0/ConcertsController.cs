using App.DAL.Contracts;
using Asp.Versioning;
using Domain.Concerts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.APIControllers.v1._0;

/// <summary>
/// 
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ConcertsController : ControllerBase
{
    private readonly IAppUOW _uow;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uow"></param>
    public ConcertsController(IAppUOW uow)
    {
        _uow = uow;
    }

    // GET: api/Concerts
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Concert>>> GetConcerts()
    {
        var concerts = await _uow.ConcertRepository.All();

        return Ok(concerts);
    }

    // GET: api/Concerts/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Concert>> GetConcert(Guid id)
    {
        var concert = await _uow.ConcertRepository.Find(id);

        if (concert == null)
        {
            return NotFound();
        }

        return concert;
    }

    // PUT: api/Concerts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="concert"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutConcert(Guid id, Concert concert)
    {
        if (id != concert.Id)
        {
            return BadRequest();
        }

        var updatedConcert = await _uow.ConcertRepository.UpdateById(id);

        if (updatedConcert == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetConcerts), new
        {
            Version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = concert.Id
        }, concert);
    }

    // POST: api/Concerts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="concert"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Concert>> PostConcert(Concert concert)
    {
        var addedConcert = await _uow.ConcertRepository.Add(concert);

        if (addedConcert == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetConcerts), new {
            Version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = concert.Id
        }, addedConcert);
    }

    // DELETE: api/Concerts/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConcert(Guid id)
    {
        var removedConcert = await _uow.ConcertRepository.RemoveById(id);

        if (removedConcert == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetConcerts), new {
            Version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id
        }, removedConcert);
    }
}