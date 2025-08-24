using HotelReservationSystem.Application.Commands.Review;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Review
{
    public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, Unit>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<UpdateReviewHandler> _logger;

        public UpdateReviewHandler(IReviewRepository reviewRepository, ILogger<UpdateReviewHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting review update for review {ReviewId}", request.ReviewId);
            var reviewId = request.ReviewId;
            var requestDto = request.RequestDto;
            var review = requestDto.Adapt<Domain.Entities.Review>();
            var result = await _reviewRepository.UpdateAsync(reviewId, review, cancellationToken);
            if (!result)
            {
                _logger.LogWarning("Review with ID {ReviewId} not found for update.", reviewId);
                throw new NotFoundException($"Review with ID {reviewId} not found.");
            }
            _logger.LogInformation("Review updated successfully for review {ReviewId}", reviewId);
            return Unit.Value;
        }
    }
}
