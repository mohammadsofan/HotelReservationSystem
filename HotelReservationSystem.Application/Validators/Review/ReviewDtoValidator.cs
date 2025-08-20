using FluentValidation;
using HotelReservationSystem.Application.Dtos.Review.Requests;
namespace HotelReservationSystem.Application.Validators.Review
{
    public class ReviewDtoValidator : AbstractValidator<ReviewRequestDto>
    {
        public ReviewDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required")
                .GreaterThan(0).WithMessage("UserId must be greater than 0");
            RuleFor(x => x.RoomId).NotEmpty().WithMessage("RoomId is required")
                .GreaterThan(0).WithMessage("RoomId must be greater than 0");
            RuleFor(x => x.Rate).NotEmpty().WithMessage("Rate is required")
                .InclusiveBetween(1, 5).WithMessage("Rate must be between 1 and 5");
        }
    }
}
