using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.User;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.User
{
    public class GetUsersByFilterHandler : IRequestHandler<GetUsersByFilterQuery, ICollection<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByFilterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ICollection<UserResponseDto>> Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var users = await _userRepository.GetAllByFilterAsync(filter);
            return users.Select(user => user.Adapt<UserResponseDto>()).ToList();
        }
    }
}
