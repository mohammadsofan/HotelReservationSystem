using HotelReservationSystem.Application.Dtos.Review.Requests;
using HotelReservationSystem.Application.Dtos.Review.Responses;
using MediatR;
namespace HotelReservationSystem.Application.Commands.Review
{
    public class CreateReviewCommand:IRequest<ReviewResponseDto>
    {
        public string UserRole { get; }
        public ReviewRequestDto RequestDto { get; }
        public CreateReviewCommand(string userRole,ReviewRequestDto requestDto)
        {
            UserRole = userRole;
            RequestDto = requestDto;
        }
    }
}
