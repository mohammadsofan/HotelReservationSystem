using FluentValidation;
using HotelReservationSystem.Application.Commands.Facility;
namespace HotelReservationSystem.Application.Validators.Facility
{
    public class CreateFacilityCommandValidator : AbstractValidator<CreateFacilityCommand>
    {
        public CreateFacilityCommandValidator()
        {
            RuleFor(c=>c.RequestDto).SetValidator(new FacilityDtoValidator());
        }
    }
}
