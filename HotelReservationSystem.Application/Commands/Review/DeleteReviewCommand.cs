using MediatR;
namespace HotelReservationSystem.Application.Commands.Review
{
    public class DeleteReviewCommand:IRequest<Unit>
    {
        public long ReviewId { get; }
        public DeleteReviewCommand(long reviewId)
        {
            ReviewId = reviewId;
        }
    }
}
