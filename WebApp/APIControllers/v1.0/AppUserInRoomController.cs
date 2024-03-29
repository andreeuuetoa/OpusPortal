using App.BLL.Contracts;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers.Rooms;
using Public.DTO.v1._0.Rooms;

namespace WebApp.APIControllers.v1._0;

/// <summary>
/// Reserve rooms for MUBA students.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]/")]
[ApiController]
[ApiVersion("1.0")]
public class AppUserInRoomController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly AppUserInRoomMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="autoMapper"></param>
    public AppUserInRoomController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new AppUserInRoomMapper(autoMapper);
    }

    // GET: api/AppUserInRoom/5
    /// <summary>
    /// Get reserved rooms for user with the specified ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUserInRoom>> GetAppUserInRoom(Guid id)
    {
        var bllAppUserInRoom = await _bll.AppUserInRoomService.Find(id);

        if (bllAppUserInRoom == null)
        {
            return NotFound();
        }

        var publicAppUserInRoom = _mapper.Map(bllAppUserInRoom);

        return Ok(publicAppUserInRoom);
    }
    
    // GET: api/AppUserInRoom/5
    /// <summary>
    /// Get reserved rooms for user with the specified ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("usersRooms/{id}")]
    [Route("api/")]
    public async Task<ActionResult<AppUserInRoom>> GetAppUserInRoomsWithUserId(Guid id)
    {
        var bllAppUsersInRoomWithUserId = await _bll.AppUserInRoomService.AllWithUserId(id);

        var res = bllAppUsersInRoomWithUserId
            .Select(appUserInRoom => _mapper.Map(appUserInRoom))
            .ToList();

        return Ok(res);
    }

    // POST: api/AppUserInRoom
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Reserve a room for user.
    /// </summary>
    /// <param name="appUserInRoom"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<AppUserInRoom>> PostAppUserInRoom(AppUserInRoom appUserInRoom)
    {
        var bllAppUserInRoom = _mapper.Map(appUserInRoom);
        if (bllAppUserInRoom == null)
        {
            return BadRequest();
        }

        var reservedRoom = await _bll.AppUserInRoomService.Add(bllAppUserInRoom);

        if (reservedRoom == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetAppUserInRoom), new { id = reservedRoom.Id }, reservedRoom);
    }

    // PUT: api/AppUserInRoom/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Edit a rooms reservation.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="appUserInRoom"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAppUserInRoom(Guid id, AppUserInRoom appUserInRoom)
    {
        if (id != appUserInRoom.Id)
        {
            return BadRequest();
        }

        var bllAppUserInRoom = _mapper.Map(appUserInRoom);
        if (bllAppUserInRoom == null)
        {
            return BadRequest();
        }

        var updatedAppUserInRoom = await _bll.AppUserInRoomService.Update(bllAppUserInRoom);
        if (updatedAppUserInRoom == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetAppUserInRoom), new {id}, updatedAppUserInRoom);
    }

    // DELETE: api/AppUserInRoom/5
    /// <summary>
    /// Delete room reservation.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppUserInRoom(Guid id)
    {
        var removedReservation = await _bll.AppUserInRoomService.Find(id);

        if (removedReservation == null)
        {
            return BadRequest();
        }

        await _bll.AppUserInRoomService.RemoveById(id);

        return Ok();
    }
}