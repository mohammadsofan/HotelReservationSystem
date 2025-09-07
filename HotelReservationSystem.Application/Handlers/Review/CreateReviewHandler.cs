using HotelReservationSystem.Application.Commands.Review;
using HotelReservationSystem.Application.Dtos.Review.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Constants;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Review
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, ReviewResponseDto>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<CreateReviewHandler> _logger;

        public CreateReviewHandler(IReviewRepository reviewRepository,
            IBookingRepository bookingRepository,
            IUserRepository userRepository,
            IRoomRepository roomRepository,
            ILogger<CreateReviewHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _logger = logger;
        }
        public async Task<ReviewResponseDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting review creation for user {UserId} and room {RoomId}", request.RequestDto.UserId, request.RequestDto.RoomId);
            var role = request.UserRole;
            var user = await _userRepository.GetOneByFilterAsync(u => u.Id == request.RequestDto.UserId);
            if (user is null)
            {
                _logger.LogWarning("User not found for review creation. UserId: {UserId}", request.RequestDto.UserId);
                throw new NotFoundException("User not found.");
            }
            var room = await _roomRepository.GetOneByFilterAsync(r => r.Id == request.RequestDto.RoomId);
            if (room is null)
            {
                _logger.LogWarning("Room not found for review creation. RoomId: {RoomId}", request.RequestDto.RoomId);
                throw new NotFoundException("Room not found.");
            }
            if(role != ApplicationRoles.Admin)
            {
                var hasBooking = await _bookingRepository
                    .GetOneByFilterAsync(b => b.UserId == request.RequestDto.UserId
                     && b.RoomId == request.RequestDto.RoomId);
                if (hasBooking == null)
                {
                    _logger.LogWarning("User {UserId} does not have a booking for room {RoomId}", request.RequestDto.UserId, request.RequestDto.RoomId);
                    throw new UnauthorizedAccessException("You must have a booking to create a review.");
                }
            }
            var existingReview = await _reviewRepository
                .GetOneByFilterAsync(r => r.UserId == request.RequestDto.UserId
                && r.RoomId == request.RequestDto.RoomId);
            if(existingReview != null)
            {
                _logger.LogWarning("User {UserId} has already reviewed room {RoomId}", request.RequestDto.UserId, request.RequestDto.RoomId);
                throw new ConflictException("You have already reviewed this room.");
            }
            var review = request.RequestDto.Adapt<Domain.Entities.Review>();
            review = await _reviewRepository.CreateAsync(review, cancellationToken);
            _logger.LogInformation("Review created successfully with ID {ReviewId}", review.Id);
            return review.Adapt<ReviewResponseDto>();
        }
    }
}
