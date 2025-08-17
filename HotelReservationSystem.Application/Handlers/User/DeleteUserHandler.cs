using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.User
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var deleted = await _userRepository.DeleteAsync(userId, cancellationToken);
            if (!deleted)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }
        }
    }
}
