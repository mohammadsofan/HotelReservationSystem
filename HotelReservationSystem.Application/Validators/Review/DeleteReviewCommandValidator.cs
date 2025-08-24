using FluentValidation;
using HotelReservationSystem.Application.Commands.Review;
namespace HotelReservationSystem.Application.Validators.Review
{
    public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidator()
        {
            RuleFor(x => x.ReviewId).NotEmpty().WithMessage("ReviewId is required")
                .GreaterThan(0).WithMessage("ReviewId must be greater than 0");
        }
    }
}
