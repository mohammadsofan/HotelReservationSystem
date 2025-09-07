using FluentValidation;
using HotelReservationSystem.Application.Commands.Room;
namespace HotelReservationSystem.Application.Validators.Room
{
    public class UpdateRoomCommandValidator:AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(c=>c.RoomId).NotEmpty().WithMessage("Room Id is required.")
                .GreaterThan(0).WithMessage("Room Id must be greater than zero");
            RuleFor(c=>c.RequestDto).SetValidator(new RoomDtoValidator());
        }
    }
}
