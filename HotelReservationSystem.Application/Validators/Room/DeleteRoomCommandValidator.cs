using FluentValidation;
using HotelReservationSystem.Application.Commands.Room;
namespace HotelReservationSystem.Application.Validators.Room
{
    public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomCommandValidator()
        {
            RuleFor(c => c.RoomId).NotEmpty().WithMessage("Room Id is required.")
                .GreaterThan(0).WithMessage("Room Id must be greater than zero");
        }
    }
}
