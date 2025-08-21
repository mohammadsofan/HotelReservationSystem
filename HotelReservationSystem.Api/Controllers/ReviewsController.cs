using HotelReservationSystem.Api.Wrappers;
using HotelReservationSystem.Application.Commands.Review;
using HotelReservationSystem.Application.Dtos.Review.Requests;
using HotelReservationSystem.Application.Dtos.Review.Responses;
using HotelReservationSystem.Application.Queries.Review;
using HotelReservationSystem.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetAllReviewsAsync()
        {
            var res = await _mediator.Send(new GetReviewsByFilterQuery());
            return Ok(ApiResponse<IEnumerable<ReviewResponseDto>>.Ok("Reviews retrieved successfully.", res));
        }

        [HttpGet("{id}", Name = "GetReviewByIdAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviewByIdAsync([FromRoute] long id)
        {
            var res = await _mediator.Send(new GetOneReviewByFilterQuery(r => r.Id == id));
            return Ok(ApiResponse<ReviewResponseDto>.Ok("Review retrieved successfully.", res));
        }
        [HttpGet("Room/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRoomReviewsAsync([FromRoute] long id)
        {
            var res = await _mediator.Send(new GetReviewsByFilterQuery(r => r.RoomId == id));
            return Ok(ApiResponse<IEnumerable<ReviewResponseDto>>.Ok("Reviews retrieved successfully.", res));
        }
        [HttpGet("User/{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetAllUserReviewsAsync([FromRoute] long id)
        {
            var res = await _mediator.Send(new GetReviewsByFilterQuery(r => r.UserId == id));
            return Ok(ApiResponse<IEnumerable<ReviewResponseDto>>.Ok("Reviews retrieved successfully.", res));
        }
        [HttpPost]
        [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Guest}")]
        public async Task<IActionResult> CreateReviewAsync([FromBody] ReviewRequestDto request)
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
            var res = await _mediator.Send(new CreateReviewCommand(role ?? ApplicationRoles.Guest, request));
            return CreatedAtRoute(nameof(GetReviewByIdAsync), new { id = res.Id }, ApiResponse<ReviewResponseDto>.Ok("Review created successfully.", res));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> UpdateReviewAsync([FromRoute] long id, [FromBody] ReviewRequestDto request)
        {
            await _mediator.Send(new UpdateReviewCommand(id, request));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> DeleteReviewAsync([FromRoute] long id)
        {
            await _mediator.Send(new DeleteReviewCommand(id));
            return NoContent();
        }
    }
}
