using HotelReservationSystem.Application.Dtos.User.Requests;
using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Domain.Constants;
using MediatR;
namespace HotelReservationSystem.Application.Commands.User
{
    public class CreateUserCommand:IRequest<UserResponseDto>
    {
        public string Role { get; }
        public CreateUserRequestDto RequestDto { get; }
        public CreateUserCommand(CreateUserRequestDto requestDto, string role = ApplicationRoles.Guest)
        {
            RequestDto = requestDto;
            Role = role;
        }

    }
}
