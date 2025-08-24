using HotelReservationSystem.Application.Dtos.User.Responses;
using MediatR;
using System.Linq.Expressions;
namespace HotelReservationSystem.Application.Queries.User
{
    public class GetUsersByFilterQuery:IRequest<ICollection<UserResponseDto>>
    {
        public Expression<Func<Domain.Entities.User, bool>>? Filter { get; }

        public GetUsersByFilterQuery(Expression<Func<Domain.Entities.User, bool>>? filter = null)
        {
            Filter = filter;
        }
    }
}
