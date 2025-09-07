using HotelReservationSystem.Application.Dtos.Review.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Review;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Review
{
    public class GetOneReviewByFilterHandler : IRequestHandler<GetOneReviewByFilterQuery, ReviewResponseDto>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<GetOneReviewByFilterHandler> _logger;

        public GetOneReviewByFilterHandler(IReviewRepository reviewRepository, ILogger<GetOneReviewByFilterHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        public async Task<ReviewResponseDto> Handle(GetOneReviewByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving single review with provided filter.");
            var filter= request.Filter;
            var review = await _reviewRepository.GetOneByFilterAsync(filter);
            if (review == null)
            {
                _logger.LogWarning("Review not found for provided filter.");
                throw new NotFoundException($"Review not found.");
            }
            _logger.LogInformation("Review found with ID {ReviewId}.", review.Id);
            return review.Adapt<ReviewResponseDto>();
        }
    }
}
