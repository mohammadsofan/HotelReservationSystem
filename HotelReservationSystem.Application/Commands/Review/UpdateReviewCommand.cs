using HotelReservationSystem.Application.Dtos.Review.Requests;
using MediatR;

namespace HotelReservationSystem.Application.Commands.Review
{
    public class UpdateReviewCommand:IRequest<Unit>
    {
        public long ReviewId { get; }
        public ReviewRequestDto RequestDto { get; }
        public UpdateReviewCommand(long reviewId, ReviewRequestDto requestDto)
        {
            ReviewId = reviewId;
            RequestDto = requestDto;
        }
    }
}
