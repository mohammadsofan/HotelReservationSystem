using HotelReservationSystem.Application.Dtos.Review.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Review;
using Mapster;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.Review
{
    public class GetOneReviewByFilterHandler : IRequestHandler<GetOneReviewByFilterQuery, ReviewResponseDto>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetOneReviewByFilterHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewResponseDto> Handle(GetOneReviewByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter= request.Filter;
            var review = await _reviewRepository.GetOneByFilterAsync(filter);
            if (review == null)
            {
                throw new NotFoundException($"Review not found.");
            }
            return review.Adapt<ReviewResponseDto>();
        }
    }
}
