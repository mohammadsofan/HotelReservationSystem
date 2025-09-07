using FluentValidation;
using HotelReservationSystem.Application.Commands.Review;
namespace HotelReservationSystem.Application.Validators.Review
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.RequestDto).SetValidator(new ReviewDtoValidator());
        }
    }
}
