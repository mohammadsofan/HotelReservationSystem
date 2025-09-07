using HotelReservationSystem.Application.Dtos.Review.Responses;
using MediatR;
using System.Linq.Expressions;
namespace HotelReservationSystem.Application.Queries.Review
{
    public class GetReviewsByFilterQuery:IRequest<ICollection<ReviewResponseDto>>
    {
        public Expression<Func<Domain.Entities.Review, bool>>? Filter { get; }
        public GetReviewsByFilterQuery(Expression<Func<Domain.Entities.Review, bool>>? filter = null)
        {
            Filter = filter;
        }
    }
}
