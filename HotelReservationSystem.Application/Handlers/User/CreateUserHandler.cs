using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHasher;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(IUserRepository userRepository,IPasswordHashingService passwordHasher, ILogger<CreateUserHandler> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }
        public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting user creation for username {Username}", request.RequestDto.Username);
            var requestDto = request.RequestDto;
            var hashedPassword = _passwordHasher.HashPassword(requestDto.Password);
            var user = requestDto.Adapt<Domain.Entities.User>();
            user.HashedPassword = hashedPassword;
            user.Role = request.Role;
            user = await _userRepository.CreateAsync(user, cancellationToken);          
            _logger.LogInformation("User created successfully with ID {UserId}", user.Id);
            return user.Adapt<UserResponseDto>();
        }
    }
}
