using Azure.Core;
using HotelReservationSystem.Api.Wrappers;
using HotelReservationSystem.Application.Commands.Review;
using HotelReservationSystem.Application.Dtos.Review.Requests;
using HotelReservationSystem.Application.Dtos.Review.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Review;
using HotelReservationSystem.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICacheService _cacheService;
        private readonly ILogger<ReviewsController> _logger;
        private readonly string AllReviewsCacheKey = "AllReviews";
        public ReviewsController(IMediator mediator,
            ICacheService cacheService,
            ILogger<ReviewsController> logger)
        {
            _mediator = mediator;
            _cacheService = cacheService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetAllReviewsAsync()
        {
            _logger.LogInformation("Getting all reviews.");
            var reviewDtos = await _cacheService.GetAsync<IEnumerable<ReviewResponseDto>>(AllReviewsCacheKey);
            if(reviewDtos != null)
            {
                _logger.LogInformation("Reviews found in cache.");
                return Ok(ApiResponse<IEnumerable<ReviewResponseDto>>.Ok("Reviews retrieved successfully.", reviewDtos));
            }
            var res = await _mediator.Send(new GetReviewsByFilterQuery());
            _logger.LogInformation("Reviews retrieved successfully.");
            await _cacheService.SetAsync<IEnumerable<ReviewResponseDto>>(AllReviewsCacheKey, res);
            _logger.LogInformation("Reviews cached successfully.");
            return Ok(ApiResponse<IEnumerable<ReviewResponseDto>>.Ok("Reviews retrieved successfully.", res));
        }

        [HttpGet("{id}", Name = "GetReviewByIdAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviewByIdAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting review by ID {ReviewId}.", id);
            var cacheKey = $"Review_{id}";
            var reviewDto = await _cacheService.GetAsync<ReviewResponseDto>(cacheKey);
            if (reviewDto != null)
            {
                _logger.LogInformation("Review found in cache.");
                return Ok(ApiResponse<ReviewResponseDto>.Ok("Review retrieved successfully.", reviewDto));
            }
            var res = await _mediator.Send(new GetOneReviewByFilterQuery(r => r.Id == id));
            _logger.LogInformation("Review retrieved successfully.");
            await _cacheService.SetAsync<ReviewResponseDto>(cacheKey, res);
            _logger.LogInformation("Review cached successfully.");
            return Ok(ApiResponse<ReviewResponseDto>.Ok("Review retrieved successfully.", res));
        }
        [HttpGet("Room/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRoomReviewsAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting reviews for room {RoomId}.", id);
            var cacheKey = $"RoomReviews_{id}";
            var reviewDtos = await _cacheService.GetAsync<IEnumerable<ReviewResponseDto>>(cacheKey);
            if (reviewDtos != null)
            {
                _logger.LogInformation("Room reviews found in cache.");
                return Ok(ApiResponse<IEnumerable<ReviewResponseDto>>.Ok("Reviews retrieved successfully.", reviewDtos));
            }
            var res = await _mediator.Send(new GetReviewsByFilterQuery(r => r.RoomId == id));
            _logger.LogInformation("Room reviews retrieved successfully.");
            await _cacheService.SetAsync<IEnumerable<ReviewResponseDto>>(cacheKey, res);
            _logger.LogInformation("Room reviews cached successfully.");
            return Ok(ApiResponse<IEnumerable<ReviewResponseDto>>.Ok("Reviews retrieved successfully.", res));
        }
        [HttpGet("User/{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetAllUserReviewsAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting reviews for user {UserId}.", id);
            var cacheKey = $"UserReviews_{id}";
            var reviewDtos = await _cacheService.GetAsync<IEnumerable<ReviewResponseDto>>(cacheKey);
            if (reviewDtos != null)
            {
                _logger.LogInformation("User reviews found in cache.");
                return Ok(ApiResponse<IEnumerable<ReviewResponseDto>>.Ok("Reviews retrieved successfully.", reviewDtos));
            }
            var res = await _mediator.Send(new GetReviewsByFilterQuery(r => r.UserId == id));
            _logger.LogInformation("User reviews retrieved successfully.");
            await _cacheService.SetAsync<IEnumerable<ReviewResponseDto>>(cacheKey, res);
            _logger.LogInformation("User reviews cached successfully.");
            return Ok(ApiResponse<IEnumerable<ReviewResponseDto>>.Ok("Reviews retrieved successfully.", res));
        }
        [HttpPost]
        [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Guest}")]
        public async Task<IActionResult> CreateReviewAsync([FromBody] ReviewRequestDto request)
        {
            _logger.LogInformation("Creating review.");
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role != ApplicationRoles.Admin)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var id))
                {
                    _logger.LogWarning("User ID not found in claims for review creation.");
                    return Unauthorized(ApiResponse.Fail("Unauthorized"));
                }
                request.UserId = id;
            }
            var res = await _mediator.Send(new CreateReviewCommand(role ?? ApplicationRoles.Guest, request));
            _logger.LogInformation("Review created successfully for user with ID {id}.", request.UserId);
            var cacheKey = AllReviewsCacheKey;
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"RoomReviews_{request.RoomId}";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"UserReviews_{request.UserId}";
            await _cacheService.RemoveAsync(cacheKey);
            _logger.LogInformation("Cache for reviews and related data removed successfully.");
            return CreatedAtRoute(nameof(GetReviewByIdAsync), new { id = res.Id }, ApiResponse<ReviewResponseDto>.Ok("Review created successfully.", res));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> UpdateReviewAsync([FromRoute] long id, [FromBody] ReviewRequestDto request)
        {
            _logger.LogInformation("Updating review with ID {ReviewId}.", id);
            await _mediator.Send(new UpdateReviewCommand(id, request));
            _logger.LogInformation("Review updated successfully.");
            var cacheKey = $"Review_{id}";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"AllReviews";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"RoomReviews_{request.RoomId}";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"UserReviews_{request.UserId}";
            await _cacheService.RemoveAsync(cacheKey);
            _logger.LogInformation("Cache for review and related data removed successfully.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> DeleteReviewAsync([FromRoute] long id)
        {
            _logger.LogInformation("Deleting review with ID {ReviewId}.", id);
            var request = await _mediator.Send(new GetOneReviewByFilterQuery(r => r.Id == id));
            await _mediator.Send(new DeleteReviewCommand(id));
            _logger.LogInformation("Review deleted successfully.");
            var cacheKey = $"Review_{id}";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"AllReviews";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"RoomReviews_{request.RoomId}";
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"UserReviews_{request.UserId}";
            await _cacheService.RemoveAsync(cacheKey);
            _logger.LogInformation("Cache for review and related data removed successfully.");
            return NoContent();
        }
    }
}
