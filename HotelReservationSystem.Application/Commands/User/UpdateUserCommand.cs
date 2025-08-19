using HotelReservationSystem.Application.Dtos.User.Requests;
using MediatR;

namespace HotelReservationSystem.Application.Commands.User
{
    public class UpdateUserCommand:IRequest<Unit>
    {
        public long UserId { get; set; }
        public UpdateUserRequestDto RequestDto { get; }
        public UpdateUserCommand(long userId,UpdateUserRequestDto requestDto)
        {
            UserId = userId;
            RequestDto = requestDto;
        }
    }
}
