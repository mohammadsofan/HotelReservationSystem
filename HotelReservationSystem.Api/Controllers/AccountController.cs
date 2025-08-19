using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequestDto request)
        {
            var res = await _mediator.Send(new LoginUserCommand(request));
            return Ok(res);
        }
    }
}
