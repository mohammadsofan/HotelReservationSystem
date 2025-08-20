using HotelReservationSystem.Application.Commands.Review;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Review
{
    public class DeleteRoomHandler : IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IReviewRepository _reviewRepository;

        public DeleteRoomHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewId = request.ReviewId;
            var deleted = await _reviewRepository.DeleteAsync(reviewId, cancellationToken);
            if(!deleted)
            {
                throw new NotFoundException($"Review with ID {reviewId} not found.");
            }
            return Unit.Value;
        }
    }
}
