using FluentValidation;
using HotelReservationSystem.Application.Commands.Room;
namespace HotelReservationSystem.Application.Validators.Room
{
    public class CreateRoomCommandValidator:AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator() {
            RuleFor(c => c.RequestDto).SetValidator(new RoomDtoValidator());
        }
    }
}
