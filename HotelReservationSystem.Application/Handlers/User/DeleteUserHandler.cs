using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.User
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand,Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DeleteUserHandler> _logger;

        public DeleteUserHandler(IUserRepository userRepository, ILogger<DeleteUserHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete user with ID {UserId}", request.UserId);
            var userId = request.UserId;
            var deleted = await _userRepository.DeleteAsync(userId, cancellationToken);
            if (!deleted)
            {
                _logger.LogWarning("User with ID {UserId} not found for deletion.", userId);
                throw new NotFoundException($"User with ID {userId} not found.");
            }
            _logger.LogInformation("User with ID {UserId} deleted successfully.", userId);
            return Unit.Value;
        }
    }
}
