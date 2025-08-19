using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand,Unit>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var existingUser = await _userRepository.GetOneByFilterAsync(u => u.Id == userId);
            if(existingUser == null)
            {
                throw new NotFoundException($"User with id {userId} not found.");
            }
            var requestDto = request.RequestDto;
            existingUser.Username = requestDto.Username;
            existingUser.FirstName = requestDto.FirstName;
            existingUser.LastName = requestDto.LastName;
            existingUser.PhoneNumber = requestDto.PhoneNumber;
            existingUser.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(existingUser, cancellationToken);
            return Unit.Value;
        }
    }
}
