using HotelReservationSystem.Application.Commands.Review;
using HotelReservationSystem.Application.Dtos.Review.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Constants;
using Mapster;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.Review
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, ReviewResponseDto>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateReviewHandler(IReviewRepository reviewRepository,
            IBookingRepository bookingRepository,
            IUserRepository userRepository,
            IRoomRepository roomRepository)
        {
            _reviewRepository = reviewRepository;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
        }
        public async Task<ReviewResponseDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var role = request.UserRole;
            var user = await _userRepository.GetOneByFilterAsync(u => u.Id == request.RequestDto.UserId);
            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }
            var room = await _roomRepository.GetOneByFilterAsync(r => r.Id == request.RequestDto.RoomId);
            if (room is null)
            {
                throw new NotFoundException("Room not found.");
            }
            if(role != ApplicationRoles.Admin)
            {
                var hasBooking = await _bookingRepository
                    .GetOneByFilterAsync(b => b.UserId == request.RequestDto.UserId
                     && b.RoomId == request.RequestDto.RoomId);
                if (hasBooking == null)
                {
                    throw new UnauthorizedAccessException("You must have a booking to create a review.");
                }
            }
            var existingReview = await _reviewRepository
                .GetOneByFilterAsync(r => r.UserId == request.RequestDto.UserId
                && r.RoomId == request.RequestDto.RoomId);
            if(existingReview != null)
            {
                throw new ConflictException("You have already reviewed this room.");
            }
            var review = request.RequestDto.Adapt<Domain.Entities.Review>();
            review = await _reviewRepository.CreateAsync(review, cancellationToken);
            return review.Adapt<ReviewResponseDto>();
        }
    }
}
