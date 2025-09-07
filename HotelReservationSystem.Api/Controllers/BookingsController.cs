using HotelReservationSystem.Api.Wrappers;
using HotelReservationSystem.Application.Commands.Booking;
using HotelReservationSystem.Application.Dtos.Booking.Requests;
using HotelReservationSystem.Application.Dtos.Booking.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Booking;
using HotelReservationSystem.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICacheService _cacheService;
        private readonly ILogger<BookingsController> _logger;
        private readonly string AllBookingsCacheKey = "AllBookings";
        public BookingsController(IMediator mediator,
            ICacheService cacheService,
            ILogger<BookingsController> logger)
        {
            _mediator = mediator;
            _cacheService = cacheService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetAllBookingsAsync()
        {
            _logger.LogInformation("Getting all bookings.");
            var bookingDtos = await _cacheService.GetAsync<IEnumerable<BookingResponseDto>>(AllBookingsCacheKey);
            if (bookingDtos != null)
            {
                _logger.LogInformation("Bookings found in cache.");
                return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", bookingDtos));
            }
            var res = await _mediator.Send(new GetBookingsByFilterQuery());
            _logger.LogInformation("Bookings retrieved successfully.");
            await _cacheService.SetAsync<IEnumerable<BookingResponseDto>>(AllBookingsCacheKey, res);
            _logger.LogInformation("Bookings cached successfully.");
            return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", res));
        }

        [HttpGet("{id}", Name = "GetBookingByIdAsync")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetBookingByIdAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting booking by ID {BookingId}.", id);
            var cacheKey = $"Booking_{id}";
            var bookingDto = await _cacheService.GetAsync<BookingResponseDto>(cacheKey);
            if (bookingDto != null)
            {
                _logger.LogInformation("Booking found in cache.");
                return Ok(ApiResponse<BookingResponseDto>.Ok("Booking retrieved successfully.", bookingDto));
            }
            var res = await _mediator.Send(new GetOneBookingByFilterQuery(b => b.Id == id));
            _logger.LogInformation("Booking retrieved successfully.");
            await _cacheService.SetAsync<BookingResponseDto>(cacheKey, res);
            _logger.LogInformation("Booking cached successfully.");
            return Ok(ApiResponse<BookingResponseDto>.Ok("Booking retrieved successfully.", res));
        }

        [HttpGet("Room/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRoomBookingsAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting bookings for room {RoomId}.", id);
            var cacheKey = $"RoomBookings_{id}";
            var bookingDtos = await _cacheService.GetAsync<IEnumerable<BookingResponseDto>>(cacheKey);
            if (bookingDtos != null)
            {
                _logger.LogInformation("Room bookings found in cache.");
                return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", bookingDtos));
            }
            var res = await _mediator.Send(new GetBookingsByFilterQuery(b => b.RoomId == id));
            _logger.LogInformation("Room bookings retrieved successfully.");
            await _cacheService.SetAsync<IEnumerable<BookingResponseDto>>(cacheKey, res);
            _logger.LogInformation("Room bookings cached successfully.");
            return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", res));
        }

        [HttpGet("User/{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetAllUserBookingsAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting bookings for user {UserId}.", id);
            var cacheKey = $"UserBookings_{id}";
            var bookingDtos = await _cacheService.GetAsync<IEnumerable<BookingResponseDto>>(cacheKey);
            if (bookingDtos != null)
            {
                _logger.LogInformation("User bookings found in cache.");
                return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", bookingDtos));
            }
            var res = await _mediator.Send(new GetBookingsByFilterQuery(b => b.UserId == id));
            _logger.LogInformation("User bookings retrieved successfully.");
            await _cacheService.SetAsync<IEnumerable<BookingResponseDto>>(cacheKey, res);
            _logger.LogInformation("User bookings cached successfully.");
            return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", res));
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserBookingsAsync()
        {
            _logger.LogInformation("Getting bookings for current logged-in user.");
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var id))
            {
                _logger.LogWarning("User ID not found in claims for current user bookings.");
                return Unauthorized(ApiResponse<string>.Fail("User ID not found in claims."));
            }
            var cacheKey = $"UserBookings_{id}";
            var bookingDtos = await _cacheService.GetAsync<IEnumerable<BookingResponseDto>>(cacheKey);
            if (bookingDtos != null)
            {
                _logger.LogInformation("Current user bookings found in cache.");
                return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", bookingDtos));
            }
            var res = await _mediator.Send(new GetBookingsByFilterQuery(b => b.UserId == id));
            _logger.LogInformation("Current user bookings retrieved successfully.");
            await _cacheService.SetAsync<IEnumerable<BookingResponseDto>>(cacheKey, res);
            _logger.LogInformation("Current user bookings cached successfully.");
            return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", res));
        }

        [HttpPost]
        [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Guest}")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] BookingRequestDto request)
        {
            _logger.LogInformation("Creating booking.");
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role != ApplicationRoles.Admin)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var id))
                {
                    _logger.LogWarning("User ID not found in claims for booking creation.");
                    return Unauthorized(ApiResponse.Fail("Unauthorized"));
                }
                request.UserId = id;
            }
            var res = await _mediator.Send(new CreateBookingCommand(request));
            _logger.LogInformation("Booking created successfully.");
            var cacheKey = AllBookingsCacheKey;
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"UserBookings_{request.UserId}";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"RoomBookings_{request.RoomId}";
            await _cacheService.RemoveAsync(cacheKey);
            _logger.LogInformation("Cache for bookings and related data removed successfully.");
            return CreatedAtRoute(nameof(GetBookingByIdAsync), new { id = res.Id }, ApiResponse<BookingResponseDto>.Ok("Booking created successfully.", res));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> UpdateBookingAsync([FromRoute] long id, [FromBody] BookingRequestDto request)
        {
            _logger.LogInformation("Updating booking with ID {BookingId}.", id);
            await _mediator.Send(new UpdateBookingCommand(id, request));
            _logger.LogInformation("Booking updated successfully.");
            var cacheKey = AllBookingsCacheKey;
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"UserBookings_{request.UserId}";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"RoomBookings_{request.RoomId}";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"Booking_{id}";
            await _cacheService.RemoveAsync(cacheKey);
            _logger.LogInformation("Cache for bookings and related data removed successfully.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> DeleteBookingAsync([FromRoute] long id)
        {
            _logger.LogInformation("Deleting booking with ID {BookingId}.", id);
            var request = await _mediator.Send(new GetOneBookingByFilterQuery(b => b.Id == id));
            await _mediator.Send(new DeleteBookingCommand(id));
            _logger.LogInformation("Booking deleted successfully.");
            var cacheKey = AllBookingsCacheKey;
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"UserBookings_{request.UserId}";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"RoomBookings_{request.RoomId}";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"Booking_{id}";
            await _cacheService.RemoveAsync(cacheKey);
            _logger.LogInformation("Cache for bookings and related data removed successfully.");
            return NoContent();
        }
    }
}
