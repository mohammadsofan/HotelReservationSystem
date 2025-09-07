using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand,Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(IUserRepository userRepository, ILogger<UpdateUserHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting user update for user {UserId}", request.UserId);
            var userId = request.UserId;
            var existingUser = await _userRepository.GetOneByFilterAsync(u => u.Id == userId);
            if(existingUser == null)
            {
                _logger.LogWarning("User with id {UserId} not found for update.", userId);
                throw new NotFoundException($"User with id {userId} not found.");
            }
            var requestDto = request.RequestDto;
            existingUser.Username = requestDto.Username;
            existingUser.FirstName = requestDto.FirstName;
            existingUser.LastName = requestDto.LastName;
            existingUser.PhoneNumber = requestDto.PhoneNumber;
            existingUser.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(userId,existingUser, cancellationToken);
            _logger.LogInformation("User updated successfully for user {UserId}", userId);
            return Unit.Value;
        }
    }
}
