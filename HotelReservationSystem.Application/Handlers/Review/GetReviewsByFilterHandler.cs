using HotelReservationSystem.Application.Dtos.Review.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Review;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Review
{
    public class GetReviewsByFilterHandler : IRequestHandler<GetReviewsByFilterQuery, ICollection<ReviewResponseDto>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewsByFilterHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ICollection<ReviewResponseDto>> Handle(GetReviewsByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var reviews = await _reviewRepository.GetAllByFilterAsync(filter);
            return reviews.Select(r => r.Adapt<ReviewResponseDto>()).ToList();
        }
    }
}
