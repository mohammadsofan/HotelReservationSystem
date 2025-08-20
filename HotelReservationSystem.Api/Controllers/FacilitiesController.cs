using Microsoft.AspNetCore.Mvc;
using MediatR;
using HotelReservationSystem.Application.Dtos.Facility.Requests;
using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Commands.Facility;
using HotelReservationSystem.Application.Queries.Facility;
using HotelReservationSystem.Api.Wrappers;
using Microsoft.AspNetCore.Authorization;
using HotelReservationSystem.Domain.Constants;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =ApplicationRoles.Admin)]
    public class FacilitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FacilitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFacilitiesAsync()
        {
            var res = await _mediator.Send(new GetFacilitiesByFilterQuery());
            return Ok(ApiResponse<IEnumerable<FacilityResponseDto>>.Ok("Facilities retrieved successfully.", res));
        }

        [HttpGet("{id}", Name = "GetFacilityByIdAsync")]
        public async Task<IActionResult> GetFacilityByIdAsync([FromRoute] long id)
        {
            var res = await _mediator.Send(new GetOneFacilityByFilterQuery(f => f.Id == id));
            return Ok(ApiResponse<FacilityResponseDto>.Ok("Facility retrieved successfully.", res));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFacilityAsync([FromBody] FacilityRequestDto request)
        {
            var res = await _mediator.Send(new CreateFacilityCommand(request));
            return CreatedAtRoute(nameof(GetFacilityByIdAsync), new { id = res.Id }, ApiResponse<FacilityResponseDto>.Ok("Facility created successfully.", res));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFacilityAsync([FromRoute] long id, [FromBody] FacilityRequestDto request)
        {
            await _mediator.Send(new UpdateFacilityCommand(id, request));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacilityAsync([FromRoute] long id)
        {
            await _mediator.Send(new DeleteFacilityCommand(id));
            return NoContent();
        }
    }
}
