using HotelReservationSystem.Api.Wrappers;
using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Dtos.Room.Requests;
using HotelReservationSystem.Application.Dtos.Room.Responses;
using HotelReservationSystem.Application.Queries.Room;
using HotelReservationSystem.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RoomsController> _logger;
        private readonly IDistributedCache _distributedCache;

        public RoomsController(IMediator mediator,
            ILogger<RoomsController> logger,
            IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _logger = logger;
            _distributedCache = distributedCache;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllRoomsAsync()
        {
            _logger.LogInformation("Getting all rooms.");
            var chachKey = "AllRooms";
            var cachedRooms = await _distributedCache.GetStringAsync(chachKey);
            if (!string.IsNullOrEmpty(cachedRooms))
            {
                _logger.LogInformation("Rooms found in cache.");
                var roomDtos = JsonSerializer.Deserialize<IEnumerable<RoomResponseDto>>(cachedRooms);
                return Ok(ApiResponse<IEnumerable<RoomResponseDto>>.Ok("Rooms retrieved successfully.", roomDtos));
            }
            var res = await _mediator.Send(new GetRoomsByFilterQuery());
            _logger.LogInformation("Rooms retrieved successfully.");
            var serializedRooms = JsonSerializer.Serialize(res);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            return Ok(ApiResponse<IEnumerable<RoomResponseDto>>.Ok("Rooms retrieved successfully.", res));
        }
        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetRoomByIdAsync")]
        public async Task<IActionResult> GetRoomByIdAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting room by ID {RoomId}.", id);
            var chachKey = $"Room_{id}";  
            var cachedRoom = await _distributedCache.GetStringAsync(chachKey);
            if (!string.IsNullOrEmpty(cachedRoom))
            {
                _logger.LogInformation("Room found in cache.");
                var roomDto = JsonSerializer.Deserialize<RoomResponseDto>(cachedRoom);
                return Ok(ApiResponse<RoomResponseDto>.Ok("Room retrieved successfully.", roomDto));
            }
            var res = await _mediator.Send(new GetOneRoomByFilterQuery(r => r.Id == id));
            _logger.LogInformation("Room retrieved successfully.");
            var serializedRoom = JsonSerializer.Serialize(res);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            await _distributedCache.SetStringAsync(chachKey, serializedRoom, options);
            _logger.LogInformation("Room cached successfully.");
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
