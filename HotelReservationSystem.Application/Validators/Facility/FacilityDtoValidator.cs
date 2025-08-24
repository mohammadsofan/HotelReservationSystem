using FluentValidation;
using HotelReservationSystem.Application.Dtos.Facility.Requests;

namespace HotelReservationSystem.Application.Validators.Facility
{
    public class FacilityDtoValidator : AbstractValidator<FacilityRequestDto>
    {
        public FacilityDtoValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("Facility name is required.")
                .Length(3,100).WithMessage("Facility name length must be between 3 and 100 characters.");
            RuleFor(f => f.Description).NotEmpty().WithMessage("Facility description is required.")
                .Length(3, 1000).WithMessage("Facility description length must be between 10 and 1000 characters.");
        }
    }
}
