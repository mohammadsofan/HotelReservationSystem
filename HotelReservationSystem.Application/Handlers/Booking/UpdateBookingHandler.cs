using HotelReservationSystem.Application.Commands.Booking;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Booking
{
    public class UpdateBookingHandler : IRequestHandler<UpdateBookingCommand, Unit>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<UpdateBookingHandler> _logger;

        public UpdateBookingHandler(IBookingRepository bookingRepository,
            IUserRepository userRepository,
            IRoomRepository roomRepository,
            ILogger<UpdateBookingHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting booking update for booking {BookingId}", request.BookingId);
            var bookingId = request.BookingId;
            var existingBooking = await _bookingRepository.GetOneByFilterAsync(b => b.Id == bookingId);
            if (existingBooking == null)
            {
                _logger.LogWarning("Booking with ID {BookingId} not found.", bookingId);
                throw new NotFoundException($"Booking with ID {bookingId} not found.");
            }
            var requestDto = request.RequestDto;
            var userId = requestDto.UserId;
            var roomId = requestDto.RoomId;
            var user = await _userRepository.GetOneByFilterAsync(b => b.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                throw new NotFoundException($"User with ID {userId} not found.");
            }
            var room = await _roomRepository.GetOneByFilterAsync(b => b.Id == roomId);
            if (room == null)
            {
                _logger.LogWarning("Room with ID {RoomId} not found.", roomId);
                throw new NotFoundException($"Room with ID {requestDto.RoomId} not found.");
            }
            if (requestDto.GuestsNumber > room.MaxOccupancy)
            {
                _logger.LogWarning("Guests number {GuestsNumber} exceeds max occupancy {MaxOccupancy} for room {RoomId}", requestDto.GuestsNumber, room.MaxOccupancy, roomId);
                throw new ConflictException($"Number of guests ({requestDto.GuestsNumber}) exceeds the room max occupancy of {room.MaxOccupancy}.");
            }
            var conflictRoomBookings = await _bookingRepository.GetAllByFilterAsync(b => b.RoomId == roomId &&
            b.Id != bookingId &&
            (requestDto.CheckIn <= b.CheckOut) &&
            (requestDto.CheckOut >= b.CheckIn));
            if (conflictRoomBookings.Any())
            {
                _logger.LogWarning("Room {RoomId} is already booked in the given date range: {CheckIn} to {CheckOut}", roomId, requestDto.CheckIn, requestDto.CheckOut);
                throw new ConflictException("Room is already booked in the given date range.");
            }
            var booking = requestDto.Adapt<Domain.Entities.Booking>();
            await _bookingRepository.UpdateAsync(bookingId, booking, cancellationToken);
            _logger.LogInformation("Booking updated successfully for booking {BookingId}", bookingId);
            return Unit.Value;
        }
    }
}
