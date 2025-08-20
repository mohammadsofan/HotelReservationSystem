using HotelReservationSystem.Application.Dtos.User.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.User;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.User
{
    public class GetOneUseByFilterHandler : IRequestHandler<GetOneUserByFilterQuery, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public GetOneUseByFilterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserResponseDto> Handle(GetOneUserByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var user = await _userRepository.GetOneByFilterAsync(filter);
            if(user is null)
            {
                throw new NotFoundException($"User not found.");
            }
            return user.Adapt<UserResponseDto>();
        }
    }
}
