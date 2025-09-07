using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.User
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly ILogger<LoginUserHandler> _logger;

        public LoginUserHandler(IUserRepository userRepository,IPasswordHashingService passwordHasher,ITokenService tokenService, ILogger<LoginUserHandler> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _logger = logger;
        }
        public async Task<LoginUserResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting login for email {Email}", request.RequestDto.Email);
            var requestDto = request.RequestDto;    
            var user = await _userRepository.GetOneByFilterAsync(u => u.Email == requestDto.Email);
            if(user == null)
            {
                _logger.LogWarning("Login failed: Invalid email {Email}", requestDto.Email);
                throw new AuthenticationException("Invalid email or password");
            }
            var isPasswordValid = _passwordHasher.VerifyHashedPassword(requestDto.Password, user.HashedPassword);
            if(!isPasswordValid)
            {
                _logger.LogWarning("Login failed: Invalid password for email {Email}", requestDto.Email);
                throw new AuthenticationException("Invalid email or password");
            }
            var token = _tokenService.GenerateToken(
                user.Id.ToString(),
                user.Username,
                user.Email,
                user.Role.ToString()
                );
            _logger.LogInformation("Login successful for user {UserId}", user.Id);
            return new LoginUserResponseDto
            {
                Token = token,
            };
        }
    }
}
