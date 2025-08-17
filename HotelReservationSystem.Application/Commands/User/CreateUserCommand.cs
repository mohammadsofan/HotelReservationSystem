using HotelReservationSystem.Application.Dtos.User.Requests;
using HotelReservationSystem.Application.Dtos.User.Responses;
using MediatR;
namespace HotelReservationSystem.Application.Commands.User
{
    public class CreateUserCommand:IRequest<UserResponseDto>
    {
        public CreateUserRequestDto RequestDto { get; }

        public CreateUserCommand(CreateUserRequestDto requestDto)
        {
            RequestDto = requestDto;
        }

    }
}
