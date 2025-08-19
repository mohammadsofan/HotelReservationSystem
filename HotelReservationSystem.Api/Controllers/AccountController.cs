using HotelReservationSystem.Api.Wrappers;
using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Requests;
using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetOneUserAsync()
        {
            var res = await _mediator.Send(new GetUsersByFilterQuery());
            return Ok(ApiResponse<IEnumerable<UserResponseDto>>.Ok("Users retrieved successfully.", res));
        }
        [HttpGet("{id}",Name = "GetOneUserAsync")]
        public async Task<IActionResult> GetOneUserAsync([FromRoute] long id)
        {
            var res = await _mediator.Send(new GetOneUserByFilterQuery(u => u.Id == id));
            return Ok(ApiResponse<UserResponseDto>.Ok("User retrieved successfully.", res));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] long id)
        {
            var res = await _mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserRequestDto request)
        {
            var res = await _mediator.Send(new LoginUserCommand(request));
            return Ok(ApiResponse<LoginUserResponseDto>.Ok("User logged in successfully.", res));
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserRequestDto request)
        {
            var res = await _mediator.Send(new CreateUserCommand(request));
            return CreatedAtRoute(nameof(GetOneUserAsync), new { id = res.Id }, ApiResponse<UserResponseDto>.Ok("User registered successfully.", res));
        }

    }
}
