using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;


namespace HotelReservationSystem.Application.Handlers.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHasher;

        public CreateUserHandler(IUserRepository userRepository,IPasswordHashingService passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;
            var hashedPassword = _passwordHasher.HashPassword(requestDto.Password);
            var user = requestDto.Adapt<Domain.Entities.User>();
            user.HashedPassword = hashedPassword;
            user = await _userRepository.CreateAsync(user, cancellationToken);          
            return user.Adapt<UserResponseDto>();
        }
    }
}
