using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var existingUser = await _userRepository.GetOneByFilterAsync(u => u.Id == userId);
            if(existingUser == null)
            {
                throw new NotFoundException($"User with id {userId} not found.");
            }
            var requestDto = request.RequestDto;
            var user = existingUser.Adapt<Domain.Entities.User>();
            user.Id = userId;
            await _userRepository.UpdateAsync(user, cancellationToken);
        }
    }
}
