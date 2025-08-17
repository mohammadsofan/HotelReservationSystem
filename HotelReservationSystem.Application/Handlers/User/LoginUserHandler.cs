using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.User
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginUserHandler(IUserRepository userRepository,IPasswordHashingService passwordHasher,ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }
        public async Task<LoginUserResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;    
            var user = await _userRepository.GetOneByFilterAsync(u => u.Email == requestDto.Email);
            if(user == null)
            {
                throw new AuthenticationException("Invalid email or password");
            }
            var isPasswordValid = _passwordHasher.VerifyHashedPassword(requestDto.Password, user.HashedPassword);
            if(!isPasswordValid)
            {
                throw new AuthenticationException("Invalid email or password");
            }
            var token = _tokenService.GenerateToken(
                user.Id.ToString(),
                user.Username,
                user.Email,
                user.Role.ToString(),
                DateTime.UtcNow.AddHours(1)
                );

            return new LoginUserResponseDto
            {
                Token = token,
            };
        }
    }
}
