using HotelReservationSystem.Application.Dtos.User.Requests;
using MediatR;

namespace HotelReservationSystem.Application.Commands.User
{
    public class UpdateUserCommand:IRequest
    {
        public long UserId { get; set; }
        public CreateUserRequestDto RequestDto { get; }
        public UpdateUserCommand(long userId,CreateUserRequestDto requestDto)
        {
            UserId = userId;

            RequestDto = requestDto;
        }
    }
}
