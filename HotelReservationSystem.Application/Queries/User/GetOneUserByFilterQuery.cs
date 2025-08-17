using HotelReservationSystem.Application.Dtos.User.Responses;
using MediatR;
using System.Linq.Expressions;

namespace HotelReservationSystem.Application.Queries.User
{
    public class GetOneUserByFilterQuery:IRequest<UserResponseDto>
    {
        public Expression<Func<Domain.Entities.User,bool>> Filter { get; }
        public GetOneUserByFilterQuery(Expression<Func<Domain.Entities.User, bool>> filter)
        {
            Filter = filter;
        }
    }
}
