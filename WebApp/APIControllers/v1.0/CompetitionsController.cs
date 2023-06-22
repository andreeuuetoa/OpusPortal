using App.DAL.Contracts;
using Asp.Versioning;
using Domain.Competitions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.APIControllers.v1._0;

/// <summary>
/// Configure the competitions that the students of MUBA can take part in.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CompetitionsController : ControllerBase
{
    private readonly IAppUOW _uow;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uow"></param>
    public CompetitionsController(IAppUOW uow)
    {
        _uow = uow;
    }

    // GET: api/Competitions
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitions()
    {
        var competitions = await _uow.CompetitionRepository.All();

        return Ok(competitions);
    }

    // GET: api/Competitions/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Competition>> GetCompetition(Guid id)
    {
        var competition = await _uow.CompetitionRepository.Find(id);

        if (competition == null)
        {
            return NotFound();
        }

        return competition;
    }

    // PUT: api/Competitions/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="competition"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompetition(Guid id, Competition competition)
    {
        if (id != competition.Id)
        {
            return BadRequest();
        }

        var updatedCompetition = await _uow.CompetitionRepository.UpdateById(id);

        if (updatedCompetition == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetCompetitions), new
        {
            Version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = competition.Id
        }, competition);
    }

    // POST: api/Competitions
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// 
    /// </summary>
    /// <param name="competition"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Competition>> PostCompetition(Competition competition)
    {
        var addedCompetition = await _uow.CompetitionRepository.Add(competition);

        if (addedCompetition == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetCompetitions), new {
            Version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = competition.Id
        }, addedCompetition);
    }

    // DELETE: api/Competitions/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompetition(Guid id)
    {
        var removedCompetition = await _uow.CompetitionRepository.RemoveById(id);

        if (removedCompetition == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetCompetition), new {
            Version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id
        }, removedCompetition);
    }
}