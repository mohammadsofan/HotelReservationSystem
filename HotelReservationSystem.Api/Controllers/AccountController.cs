using HotelReservationSystem.Api.Wrappers;
using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Requests;
using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Queries.User;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles=ApplicationRoles.Admin)]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICacheService _cacheService;
        private readonly ILogger<AccountController> _logger;
        private readonly string AllUsersCacheKey = "AllUsers";
        public AccountController(IMediator mediator, ICacheService cacheService, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _cacheService = cacheService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            _logger.LogInformation("Getting all users.");
            var userDtos = await _cacheService.GetAsync<IEnumerable<UserResponseDto>>(AllUsersCacheKey);
            if (userDtos != null)
            {
                _logger.LogInformation("Users found in cache.");
                return Ok(ApiResponse<IEnumerable<UserResponseDto>>.Ok("Users retrieved successfully.", userDtos));
            }
            var res = await _mediator.Send(new GetUsersByFilterQuery());
            _logger.LogInformation("Users retrieved successfully.");
            await _cacheService.SetAsync<IEnumerable<UserResponseDto>>(AllUsersCacheKey, res);
            _logger.LogInformation("Users cached successfully.");
            return Ok(ApiResponse<IEnumerable<UserResponseDto>>.Ok("Users retrieved successfully.", res));
        }
        [HttpGet("{id}",Name = "GetUserByIdAsync")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting user by ID {UserId}.", id);
            var cacheKey = $"User_{id}";
            var userDto = await _cacheService.GetAsync<UserResponseDto>(cacheKey);
            if (userDto != null)
            {
                _logger.LogInformation("User found in cache.");
                return Ok(ApiResponse<UserResponseDto>.Ok("User retrieved successfully.", userDto));
            }
            var res = await _mediator.Send(new GetOneUserByFilterQuery(u => u.Id == id));
            _logger.LogInformation("User retrieved successfully.");
            await _cacheService.SetAsync<UserResponseDto>(cacheKey, res);
            _logger.LogInformation("User cached successfully.");
            return Ok(ApiResponse<UserResponseDto>.Ok("User retrieved successfully.", res));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] long id)
        {
            _logger.LogInformation("Deleting user with ID {UserId}.", id);
            var res = await _mediator.Send(new DeleteUserCommand(id));
            _logger.LogInformation("User deleted successfully.");
            var cacheKey = $"User_{id}";
            await _cacheService.RemoveAsync(cacheKey);
            await _cacheService.RemoveAsync(AllUsersCacheKey);
            _logger.LogInformation("User cache removed successfully.");
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] long id, [FromBody] UpdateUserRequestDto user)
        {
            _logger.LogInformation("Updating user with ID {UserId}.", id);
            await _mediator.Send(new UpdateUserCommand(id, user));
            _logger.LogInformation("User updated successfully.");
            var cacheKey = $"User_{id}";
            await _cacheService.RemoveAsync(cacheKey);
            await _cacheService.RemoveAsync(AllUsersCacheKey);
            _logger.LogInformation("User cache removed successfully.");
            return NoContent();
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserRequestDto request)
        {
            _logger.LogInformation("Logging in user with email {Email}.", request.Email);
            var res = await _mediator.Send(new LoginUserCommand(request));
            _logger.LogInformation("User logged in successfully.");
            return Ok(ApiResponse<LoginUserResponseDto>.Ok("User logged in successfully.", res));
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserRequestDto request)
        {
            _logger.LogInformation("Registering user with email {Email}.", request.Email);
            var res = await _mediator.Send(new CreateUserCommand(request));
            _logger.LogInformation("User registered successfully.");
            var cacheKey = $"User_{res.Id}";
            await _cacheService.RemoveAsync(AllUsersCacheKey);
            await _cacheService.RemoveAsync(cacheKey);
            _logger.LogInformation("User cache removed successfully.");
            return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = res.Id }, ApiResponse<UserResponseDto>.Ok("User registered successfully.", res));
        }

    }
}
