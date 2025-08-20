using HotelReservationSystem.Application.Commands.Review;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Review
{
    public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, Unit>
    {
        private readonly IReviewRepository _reviewRepository;

        public UpdateReviewHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewId = request.ReviewId;
            var requestDto = request.RequestDto;
            var review = requestDto.Adapt<Domain.Entities.Review>();
            var result = await _reviewRepository.UpdateAsync(reviewId, review, cancellationToken);
            if (!result)
            {
                throw new NotFoundException($"Review with ID {reviewId} not found.");
            }
            return Unit.Value;
        }
    }
}
