using HotelReservationSystem.Application.Dtos.Facility.Responses;
using MediatR;
using System.Linq.Expressions;
namespace HotelReservationSystem.Application.Queries.Facility
{
    public class GetFacilitiesByFilterQuery:IRequest<ICollection<FacilityResponseDto>>
    {
        public Expression<Func<Domain.Entities.Facility, bool>>? Filter { get; }

        public GetFacilitiesByFilterQuery(Expression<Func<Domain.Entities.Facility, bool>>? filter = null)
        {
            Filter = filter;
        }
    }
}
