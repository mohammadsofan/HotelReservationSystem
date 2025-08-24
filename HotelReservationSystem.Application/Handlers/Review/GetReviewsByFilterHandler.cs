using HotelReservationSystem.Application.Dtos.Review.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Review;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Review
{
    public class GetReviewsByFilterHandler : IRequestHandler<GetReviewsByFilterQuery, ICollection<ReviewResponseDto>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<GetReviewsByFilterHandler> _logger;

        public GetReviewsByFilterHandler(IReviewRepository reviewRepository, ILogger<GetReviewsByFilterHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        public async Task<ICollection<ReviewResponseDto>> Handle(GetReviewsByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving reviews with provided filter.");
            var filter = request.Filter;
            var reviews = await _reviewRepository.GetAllByFilterAsync(filter);
            _logger.LogInformation("Retrieved {Count} reviews.", reviews.Count());
            return reviews.Select(r => r.Adapt<ReviewResponseDto>()).ToList();
        }
    }
}
