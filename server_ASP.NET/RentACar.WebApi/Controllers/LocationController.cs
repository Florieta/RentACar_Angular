using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Locations.Commands.Create;
using RentACar.Application.Locations.Queries;
using RentACar.Domain.Entitites;
using RentACar.WebApi.ViewModels.Location;
using AutoMapper;
using MediatR;
using RentACar.WebApi.Middleware;
using RentACar.Api.Logger;

namespace RentACar.WebApi.Controllers
{
    /// <summary>
    /// All location methods
    /// </summary>
    
    [Route("api/Location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public LocationController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all location from the database
        /// </summary>
        /// <returns>Returns a list of all locations</returns>
        /// <response code="200">Gets all locations</response>
        
        [HttpGet]
        public async Task<IActionResult> All()
        {
            Log.Instance.LogInformation("Retrieving the list of locations");

            GetAllLocations query = new GetAllLocations();
            List<Location> result = await _mediator.Send(query);
            List<GetLocationViewModel> mappedResult = _mapper.Map<List<GetLocationViewModel>>(result);

            Log.Instance.LogInformation($"There are {result.Count} locations in the fleet");

            return Ok(mappedResult);
        }

        /// <summary>
        /// Get a certain location by Id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A certain instance of location</returns>
        /// <response code="200">Gets a location by identifier.</response>
        /// <response code="404">If the location was not found.</response>
        
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GetLocationById query = new GetLocationById()
            {
                Id = id
            };

            Log.Instance.LogInformation("Retrieving the location by Id");

            Location location = await _mediator.Send(query);

            if (location == null)
            {
                Log.Instance.LogWarning("The Id could not be found");
                return NotFound();
            }

            GetLocationViewModel getLocationModel = _mapper.Map<GetLocationViewModel>(location);
            return Ok(getLocationModel);
        }

        /// <summary>
        /// Create a location and adds it to the database
        /// </summary>
        /// <param name="addLocationModel"></param>
        /// <returns>The new created location</returns>
        /// <response code="200">Gets the created location</response>

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddLocationModel addLocationModel)
        {
            CreateLocation command = _mapper.Map<CreateLocation>(addLocationModel);
            Location location = await _mediator.Send(command);
            GetLocationViewModel getLocationModel = _mapper.Map<GetLocationViewModel>(location);

            Log.Instance.LogInformation($"A new {location.LocationName} was created  at {DateTime.Now.TimeOfDay}");

            return CreatedAtAction(nameof(GetById), new { locationId = location.Id }, getLocationModel);
        }

    }
}
