using FluentValidation;
using HotelReservationSystem.Application.Commands.Facility;
namespace HotelReservationSystem.Application.Validators.Facility
{
    public class DeleteFacilityCommandValidator : AbstractValidator<DeleteFacilityCommand>
    {
        public DeleteFacilityCommandValidator()
        {
            RuleFor(c => c.FacilityId)
                .NotEmpty().WithMessage("Facility ID is required.")
                .GreaterThan(0).WithMessage("Facility ID must be greater than zero.");
        }
    }
}
