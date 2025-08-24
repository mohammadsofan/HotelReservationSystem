using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.User;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.User
{
    public class GetUsersByFilterHandler : IRequestHandler<GetUsersByFilterQuery, ICollection<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUsersByFilterHandler> _logger;

        public GetUsersByFilterHandler(IUserRepository userRepository, ILogger<GetUsersByFilterHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<ICollection<UserResponseDto>> Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving users with provided filter.");
            var filter = request.Filter;
            var users = await _userRepository.GetAllByFilterAsync(filter);
            _logger.LogInformation("Retrieved {Count} users.", users.Count());
            return users.Select(user => user.Adapt<UserResponseDto>()).ToList();
        }
    }
}
