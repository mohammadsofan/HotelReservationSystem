using HotelReservationSystem.Application.Dtos.User.Requests;
using HotelReservationSystem.Application.Dtos.User.Responses;
using MediatR;


namespace HotelReservationSystem.Application.Commands.User
{
    public class LoginUserCommand:IRequest<LoginUserResponseDto>
    {
        public LoginUserRequestDto RequestDto { get; }
        public LoginUserCommand(LoginUserRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
}
