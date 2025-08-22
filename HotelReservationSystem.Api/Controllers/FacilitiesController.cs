using Microsoft.AspNetCore.Mvc;
using MediatR;
using HotelReservationSystem.Application.Dtos.Facility.Requests;
using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Commands.Facility;
using HotelReservationSystem.Application.Queries.Facility;
using HotelReservationSystem.Api.Wrappers;
using Microsoft.AspNetCore.Authorization;
using HotelReservationSystem.Domain.Constants;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =ApplicationRoles.Admin)]
    public class FacilitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FacilitiesController> _logger;

        public FacilitiesController(IMediator mediator, ILogger<FacilitiesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFacilitiesAsync()
        {
            _logger.LogInformation("Getting all facilities.");
            var res = await _mediator.Send(new GetFacilitiesByFilterQuery());
            _logger.LogInformation("Facilities retrieved successfully.");
            return Ok(ApiResponse<IEnumerable<FacilityResponseDto>>.Ok("Facilities retrieved successfully.", res));
        }

        [HttpGet("{id}", Name = "GetFacilityByIdAsync")]
        public async Task<IActionResult> GetFacilityByIdAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting facility by ID {FacilityId}.", id);
            var res = await _mediator.Send(new GetOneFacilityByFilterQuery(f => f.Id == id));
            _logger.LogInformation("Facility retrieved successfully.");
            return Ok(ApiResponse<FacilityResponseDto>.Ok("Facility retrieved successfully.", res));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFacilityAsync([FromBody] FacilityRequestDto request)
        {
            _logger.LogInformation("Creating facility.");
            var res = await _mediator.Send(new CreateFacilityCommand(request));
            _logger.LogInformation("Facility created successfully.");
            return CreatedAtRoute(nameof(GetFacilityByIdAsync), new { id = res.Id }, ApiResponse<FacilityResponseDto>.Ok("Facility created successfully.", res));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFacilityAsync([FromRoute] long id, [FromBody] FacilityRequestDto request)
        {
            _logger.LogInformation("Updating facility with ID {FacilityId}.", id);
            await _mediator.Send(new UpdateFacilityCommand(id, request));
            _logger.LogInformation("Facility updated successfully.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacilityAsync([FromRoute] long id)
        {
            _logger.LogInformation("Deleting facility with ID {FacilityId}.", id);
            await _mediator.Send(new DeleteFacilityCommand(id));
            _logger.LogInformation("Facility deleted successfully.");
            return NoContent();
        }
    }
}
