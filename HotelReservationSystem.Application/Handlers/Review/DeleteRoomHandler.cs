using HotelReservationSystem.Application.Commands.Review;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Review
{
    public class DeleteRoomHandler : IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<DeleteRoomHandler> _logger;

        public DeleteRoomHandler(IReviewRepository reviewRepository, ILogger<DeleteRoomHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete review with ID {ReviewId}", request.ReviewId);
            var reviewId = request.ReviewId;
            var deleted = await _reviewRepository.DeleteAsync(reviewId, cancellationToken);
            if(!deleted)
            {
                _logger.LogWarning("Review with ID {ReviewId} not found for deletion.", reviewId);
                throw new NotFoundException($"Review with ID {reviewId} not found.");
            }
            _logger.LogInformation("Review with ID {ReviewId} deleted successfully.", reviewId);
            return Unit.Value;
        }
    }
}
