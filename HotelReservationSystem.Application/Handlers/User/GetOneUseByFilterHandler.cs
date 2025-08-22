using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.User;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.User
{
    public class GetOneUseByFilterHandler : IRequestHandler<GetOneUserByFilterQuery, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetOneUseByFilterHandler> _logger;

        public GetOneUseByFilterHandler(IUserRepository userRepository, ILogger<GetOneUseByFilterHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<UserResponseDto> Handle(GetOneUserByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving single user with provided filter.");
            var filter = request.Filter;
            var user = await _userRepository.GetOneByFilterAsync(filter);
            if(user is null)
            {
                _logger.LogWarning("User not found for provided filter.");
                throw new NotFoundException($"User not found.");
            }
            _logger.LogInformation("User found with ID {UserId}.", user.Id);
            return user.Adapt<UserResponseDto>();
        }
    }
}
