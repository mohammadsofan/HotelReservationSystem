using MediatR;
namespace HotelReservationSystem.Application.Commands.User
{
    public class DeleteUserCommand:IRequest<Unit>
    {
        public long UserId { get; }
        public DeleteUserCommand(long userId)
        {
            UserId = userId;
        }
    }
}
