using HotelReservationSystem.Application.Commands.Booking;
using HotelReservationSystem.Application.Dtos.Booking.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Booking
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingResponseDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<CreateBookingHandler> _logger;

        public CreateBookingHandler(IBookingRepository bookingRepository,
            IUserRepository userRepository,
            IRoomRepository roomRepository,
            ILogger<CreateBookingHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _logger = logger;
        }
        public async Task<BookingResponseDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting booking creation for user {UserId} and room {RoomId}", request.RequestDto.UserId, request.RequestDto.RoomId);
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
            var conflictRoomBookings = await _bookingRepository.GetAllByFilterAsync(b => b.RoomId == roomId
            && (requestDto.CheckIn <= b.CheckOut)
            && (requestDto.CheckOut >= b.CheckIn));

            if (conflictRoomBookings.Any())
            {
                _logger.LogWarning("Room {RoomId} is already booked in the given date range: {CheckIn} to {CheckOut}", roomId, requestDto.CheckIn, requestDto.CheckOut);
                throw new ConflictException("Room is already booked in the given date range.");
            }

            var booking = requestDto.Adapt<Domain.Entities.Booking>();
            booking = await _bookingRepository.CreateAsync(booking, cancellationToken);
            _logger.LogInformation("Booking created successfully with ID {BookingId}", booking.Id);
            return booking.Adapt<BookingResponseDto>();
        }
    }
}
