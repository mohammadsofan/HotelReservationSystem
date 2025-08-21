using HotelReservationSystem.Application.Commands.Booking;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.Booking
{
    public class UpdateBookingHandler : IRequestHandler<UpdateBookingCommand, Unit>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;

        public UpdateBookingHandler(IBookingRepository bookingRepository,
            IUserRepository userRepository,
            IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
        }

        public async Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var bookingId = request.BookingId;
            var existingBooking = await _bookingRepository.GetOneByFilterAsync(b => b.Id == bookingId);
            if (existingBooking == null)
            {
                throw new NotFoundException($"Booking with ID {bookingId} not found.");
            }
            var requestDto = request.RequestDto;
            var userId = requestDto.UserId;
            var roomId = requestDto.RoomId;
            var user = await _userRepository.GetOneByFilterAsync(b => b.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }
            var room = await _roomRepository.GetOneByFilterAsync(b => b.Id == roomId);
            if (room == null)
            {
                throw new NotFoundException($"Room with ID {requestDto.RoomId} not found.");
            }
            if (requestDto.GuestsNumber > room.MaxOccupancy)
            {
                throw new ConflictException($"Number of guests ({requestDto.GuestsNumber}) exceeds the room max occupancy of {room.MaxOccupancy}.");
            }
            var conflictRoomBookings = await _bookingRepository.GetAllByFilterAsync(b => b.RoomId == roomId &&
            b.Id != bookingId &&
            (requestDto.CheckIn <= b.CheckOut) &&
            (requestDto.CheckOut >= b.CheckIn));
            if (conflictRoomBookings.Any())
            {
                throw new ConflictException("Room is already booked in the given date range.");
            }
            var booking = requestDto.Adapt<Domain.Entities.Booking>();
            await _bookingRepository.UpdateAsync(bookingId, booking, cancellationToken);
            return Unit.Value;
        }
    }
}
