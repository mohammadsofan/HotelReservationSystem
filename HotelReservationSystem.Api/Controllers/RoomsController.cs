using Microsoft.AspNetCore.Mvc; 
using MediatR;
using HotelReservationSystem.Application.Dtos.Room.Requests;
using HotelReservationSystem.Application.Dtos.Room.Responses;
using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Queries.Room;
using HotelReservationSystem.Api.Wrappers;
using Microsoft.AspNetCore.Authorization;
using HotelReservationSystem.Domain.Constants;
using HotelReservationSystem.Application.Dtos.Facility.Responses;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RoomsController> _logger;

        public RoomsController(IMediator mediator, ILogger<RoomsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllRoomsAsync()
        {
            _logger.LogInformation("Getting all rooms.");
            var res = await _mediator.Send(new GetRoomsByFilterQuery());
            _logger.LogInformation("Rooms retrieved successfully.");
            return Ok(ApiResponse<IEnumerable<RoomResponseDto>>.Ok("Rooms retrieved successfully.", res));
        }
        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetRoomByIdAsync")]
        public async Task<IActionResult> GetRoomByIdAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting room by ID {RoomId}.", id);
            var res = await _mediator.Send(new GetOneRoomByFilterQuery(r=>r.Id==id));
            _logger.LogInformation("Room retrieved successfully.");
            return Ok(ApiResponse<RoomResponseDto>.Ok("Room retrieved successfully.", res));
        }
        [HttpGet("{roomId}/facilities")]
        public async Task<IActionResult> GetRoomFacilitiesAsync([FromRoute] long roomId)
        {
            _logger.LogInformation("Getting facilities for room {RoomId}.", roomId);
            var res = await _mediator.Send(new GetAllRoomFacilitiesQuery(roomId));
            _logger.LogInformation("Room facilities retrieved successfully.");
            return Ok(ApiResponse<IEnumerable<FacilityResponseDto>>.Ok("Room facilities retrieved successfully.", res));
        }
        [HttpPost("{roomId}/facility/{facilityId}")]
        public async Task<IActionResult> AssignFacilityToRoomAsync([FromRoute] long roomId, [FromRoute] long facilityId)
        {
            _logger.LogInformation("Assigning facility {FacilityId} to room {RoomId}.", facilityId, roomId);
            await _mediator.Send(new AssignFacilityToRoomCommand(roomId, facilityId));
            _logger.LogInformation("Facility assigned to room successfully.");
            return Created();
        }
        [HttpDelete("{roomId}/facility/{facilityId}")]
        public async Task<IActionResult> RemoveFacilityFromRoomAsync([FromRoute] long roomId, [FromRoute] long facilityId)
        {
            _logger.LogInformation("Removing facility {FacilityId} from room {RoomId}.", facilityId, roomId);
            await _mediator.Send(new RemoveFacilityFromRoomCommand(roomId, facilityId));
            _logger.LogInformation("Facility removed from room successfully.");
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoomAsync([FromBody] RoomRequestDto request)
        {
            _logger.LogInformation("Creating room.");
            var res = await _mediator.Send(new CreateRoomCommand(request));
            _logger.LogInformation("Room created successfully.");
            return CreatedAtRoute(nameof(GetRoomByIdAsync), new { id = res.Id }, ApiResponse<RoomResponseDto>.Ok("Room created successfully.", res));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomAsync([FromRoute] long id, [FromBody] RoomRequestDto request)
        {
            _logger.LogInformation("Updating room {RoomId}.", id);
            await _mediator.Send(new UpdateRoomCommand(id, request));
            _logger.LogInformation("Room updated successfully.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAsync([FromRoute] long id)
        {
            _logger.LogInformation("Deleting room {RoomId}.", id);
            await _mediator.Send(new DeleteRoomCommand(id));
            _logger.LogInformation("Room deleted successfully.");
            return NoContent();
        }
    }
}
