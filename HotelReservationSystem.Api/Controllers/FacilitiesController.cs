using HotelReservationSystem.Api.Wrappers;
using HotelReservationSystem.Application.Commands.Facility;
using HotelReservationSystem.Application.Dtos.Facility.Requests;
using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Facility;
using HotelReservationSystem.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class FacilitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICacheService _cacheService;
        private readonly ILogger<FacilitiesController> _logger;
        private readonly string AllFacilitiesCacheKey = "AllFacilities";
        public FacilitiesController(IMediator mediator,
            ICacheService cacheService,
            ILogger<FacilitiesController> logger)
        {
            _mediator = mediator;
            _cacheService = cacheService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFacilitiesAsync()
        {
            _logger.LogInformation("Getting all facilities.");
            var facilityDtos = await _cacheService.GetAsync<IEnumerable<FacilityResponseDto>>(AllFacilitiesCacheKey);
            if (facilityDtos != null)
            {
                _logger.LogInformation("Facilities found in cache.");
                return Ok(ApiResponse<IEnumerable<FacilityResponseDto>>.Ok("Facilities retrieved successfully.", facilityDtos));

            }
            var res = await _mediator.Send(new GetFacilitiesByFilterQuery());
            _logger.LogInformation("Facilities retrieved successfully.");
            await _cacheService.SetAsync<IEnumerable<FacilityResponseDto>>(AllFacilitiesCacheKey, res);
            _logger.LogInformation("Facilities cached successfully.");
            return Ok(ApiResponse<IEnumerable<FacilityResponseDto>>.Ok("Facilities retrieved successfully.", res));
        }

        [HttpGet("{id}", Name = "GetFacilityByIdAsync")]
        public async Task<IActionResult> GetFacilityByIdAsync([FromRoute] long id)
        {
            _logger.LogInformation("Getting facility by ID {FacilityId}.", id);
            var cacheKey = $"Facility_{id}";
            var facilityDto = await _cacheService.GetAsync<FacilityResponseDto>(cacheKey);
            if (facilityDto != null)
            {
                _logger.LogInformation("Facility found in cache.");
                return Ok(ApiResponse<FacilityResponseDto>.Ok("Facility retrieved successfully.", facilityDto));
            }
            var res = await _mediator.Send(new GetOneFacilityByFilterQuery(f => f.Id == id));
            _logger.LogInformation("Facility retrieved successfully.");
            await _cacheService.SetAsync<FacilityResponseDto>(cacheKey, res);
            _logger.LogInformation("Facility cached successfully.");

            return Ok(ApiResponse<FacilityResponseDto>.Ok("Facility retrieved successfully.", res));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFacilityAsync([FromBody] FacilityRequestDto request)
        {
            _logger.LogInformation("Creating facility.");
            var res = await _mediator.Send(new CreateFacilityCommand(request));
            _logger.LogInformation("Facility created successfully.");
            await _cacheService.RemoveAsync(AllFacilitiesCacheKey);
            _logger.LogInformation("Cache for Facilities removed successfully.");
            return CreatedAtRoute(nameof(GetFacilityByIdAsync), new { id = res.Id }, ApiResponse<FacilityResponseDto>.Ok("Facility created successfully.", res));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFacilityAsync([FromRoute] long id, [FromBody] FacilityRequestDto request)
        {
            _logger.LogInformation("Updating facility with ID {FacilityId}.", id);
            await _mediator.Send(new UpdateFacilityCommand(id, request));
            _logger.LogInformation("Facility updated successfully.");
            var cacheKey = AllFacilitiesCacheKey;
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"Facility_{id}";
            await _cacheService.RemoveAsync(cacheKey);
            _logger.LogInformation("Cache for Facilities and related data removed successfully.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacilityAsync([FromRoute] long id)
        {
            _logger.LogInformation("Deleting facility with ID {FacilityId}.", id);
            await _mediator.Send(new DeleteFacilityCommand(id));
            _logger.LogInformation("Facility deleted successfully.");
            var cacheKey = AllFacilitiesCacheKey;
            await _cacheService.RemoveAsync(cacheKey);
            cacheKey = $"Facility_{id}";
            await _cacheService.RemoveAsync(cacheKey);
            _logger.LogInformation("Cache for Facilities and related data removed successfully.");
            return NoContent();
        }
    }
}
