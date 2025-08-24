using FluentValidation;
using HotelReservationSystem.Application.Commands.Review;
namespace HotelReservationSystem.Application.Validators.Review
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(x => x.ReviewId).NotEmpty().WithMessage("ReviewId is required")
                .GreaterThan(0).WithMessage("ReviewId must be greater than 0");
            RuleFor(x => x.RequestDto).SetValidator(new ReviewDtoValidator());
        }
    }
}
