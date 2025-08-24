using HotelReservationSystem.Api.Wrappers;
using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Requests;
using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Queries.User;
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
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            _logger.LogInformation("Getting all users.");
            var res = await _mediator.Send(new GetUsersByFilterQuery());
            _logger.LogInformation("Users retrieved successfully.");
            return Ok(ApiResponse<IEnumerable<UserResponseDto>>.Ok("Users retrieved successfully.", res));
        }
        [HttpGet("{id}",Name = "GetUserByIdAsync")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting user by ID {UserId}.", id);
            var res = await _mediator.Send(new GetOneUserByFilterQuery(u => u.Id == id));
            _logger.LogInformation("User retrieved successfully.");
            return Ok(ApiResponse<UserResponseDto>.Ok("User retrieved successfully.", res));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] long id)
        {
            _logger.LogInformation("Deleting user with ID {UserId}.", id);
            var res = await _mediator.Send(new DeleteUserCommand(id));
            _logger.LogInformation("User deleted successfully.");
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] long id, [FromBody] UpdateUserRequestDto user)
        {
            _logger.LogInformation("Updating user with ID {UserId}.", id);
            await _mediator.Send(new UpdateUserCommand(id, user));
            _logger.LogInformation("User updated successfully.");
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
            return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = res.Id }, ApiResponse<UserResponseDto>.Ok("User registered successfully.", res));
        }

    }
}
