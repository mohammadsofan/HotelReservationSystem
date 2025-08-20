using HotelReservationSystem.Application.Dtos.Facility.Responses;
using MediatR;
using System.Linq.Expressions;

namespace HotelReservationSystem.Application.Queries.Facility
{
    public class GetOneFacilityByFilterQuery:IRequest<FacilityResponseDto>
    {
        public Expression<Func<Domain.Entities.Facility, bool>> Filter { get; }

        public GetOneFacilityByFilterQuery(Expression<Func<Domain.Entities.Facility, bool>> filter)
        {
            Filter = filter;
        }
    }
}
