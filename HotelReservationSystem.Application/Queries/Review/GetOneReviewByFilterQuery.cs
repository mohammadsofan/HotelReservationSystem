using HotelReservationSystem.Application.Dtos.Review.Responses;
using MediatR;
using System.Linq.Expressions;
namespace HotelReservationSystem.Application.Queries.Review
{
    public class GetOneReviewByFilterQuery:IRequest<ReviewResponseDto>
    {
        public Expression<Func<Domain.Entities.Review, bool>> Filter { get; }

        public GetOneReviewByFilterQuery(Expression<Func<Domain.Entities.Review, bool>> filter)
        {
            Filter = filter;
        }
    }
}
