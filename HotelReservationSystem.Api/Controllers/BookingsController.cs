using HotelReservationSystem.Api.Wrappers;
using HotelReservationSystem.Application.Commands.Booking;
using HotelReservationSystem.Application.Dtos.Booking.Requests;
using HotelReservationSystem.Application.Dtos.Booking.Responses;
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

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetAllBookingsAsync()
        {
            var res = await _mediator.Send(new GetBookingsByFilterQuery());
            return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", res));
        }

        [HttpGet("{id}", Name = "GetBookingByIdAsync")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetBookingByIdAsync([FromRoute] long id)
        {
            var res = await _mediator.Send(new GetOneBookingByFilterQuery(b => b.Id == id));
            return Ok(ApiResponse<BookingResponseDto>.Ok("Booking retrieved successfully.", res));
        }

        [HttpGet("Room/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRoomBookingsAsync([FromRoute] long id)
        {
            var res = await _mediator.Send(new GetBookingsByFilterQuery(b => b.RoomId == id));
            return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", res));
        }

        [HttpGet("User/{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetAllUserBookingsAsync([FromRoute] long id)
        {
            var res = await _mediator.Send(new GetBookingsByFilterQuery(b => b.UserId == id));
            return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", res));
        }

        [HttpPost]
        [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Guest}")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] BookingRequestDto request)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role != ApplicationRoles.Admin)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var id))
                {
                    return Unauthorized(ApiResponse.Fail("Unauthorized"));
                }
                request.UserId = id;
            }
            var res = await _mediator.Send(new CreateBookingCommand(request));
            return CreatedAtRoute(nameof(GetBookingByIdAsync), new { id = res.Id }, ApiResponse<BookingResponseDto>.Ok("Booking created successfully.", res));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> UpdateBookingAsync([FromRoute] long id, [FromBody] BookingRequestDto request)
        {
            await _mediator.Send(new UpdateBookingCommand(id, request));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> DeleteBookingAsync([FromRoute] long id)
        {
            await _mediator.Send(new DeleteBookingCommand(id));
            return NoContent();
        }

        [HttpGet("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserBookingsAsync()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var id))
            {
                return Unauthorized(ApiResponse.Fail("Unauthorized"));
            }
            var res = await _mediator.Send(new GetBookingsByFilterQuery(b => b.UserId == id));
            return Ok(ApiResponse<IEnumerable<BookingResponseDto>>.Ok("Bookings retrieved successfully.", res));
        }
    }
}
