using Microsoft.AspNetCore.Mvc; 
using MediatR;
using HotelReservationSystem.Application.Dtos.Room.Requests;
using HotelReservationSystem.Application.Dtos.Room.Responses;
using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Queries.Room;
using HotelReservationSystem.Api.Wrappers;
using Microsoft.AspNetCore.Authorization;
using HotelReservationSystem.Domain.Constants;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllRoomsAsync()
        {
            var res = await _mediator.Send(new GetRoomsByFilterQuery());
            return Ok(ApiResponse<IEnumerable<RoomResponseDto>>.Ok("Rooms retrieved successfully.", res));
        }
        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetRoomByIdAsync")]
        public async Task<IActionResult> GetRoomByIdAsync([FromRoute] long id)
        {
            var res = await _mediator.Send(new GetOneRoomByFilterQuery(r=>r.Id==id));
            return Ok(ApiResponse<RoomResponseDto>.Ok("Room retrieved successfully.", res));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoomAsync([FromBody] RoomRequestDto request)
        {
            var res = await _mediator.Send(new CreateRoomCommand(request));
            return CreatedAtRoute(nameof(GetRoomByIdAsync), new { id = res.Id }, ApiResponse<RoomResponseDto>.Ok("Room created successfully.", res));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomAsync([FromRoute] long id, [FromBody] RoomRequestDto request)
        {
            await _mediator.Send(new UpdateRoomCommand(id, request));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAsync([FromRoute] long id)
        {
            await _mediator.Send(new DeleteRoomCommand(id));
            return NoContent();
        }
    }
}
