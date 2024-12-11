using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Api.Logger;
using RentACar.Application.Ratings.Commands.Create;
using RentACar.Application.Ratings.Queries;
using RentACar.Domain.Entitites;
using RentACar.WebApi.ViewModels.Rating;

namespace RentACar.WebApi.Controllers
{
    /// <summary>
    /// All rating methods
    /// </summary>
    
    [Route("api/Rating")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public RatingController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all ratings about a certain car by carId
        /// </summary>
        /// <param name="carId"></param>
        /// <returns>A list of all ratings</returns>
        /// <response code="200">Gets all ratings by identifier.</response>
        
        [HttpGet]
        [Route("Car/{carId}")]
        public async Task<IActionResult> GetByCarId(int carId)
        {
            Log.Instance.LogInformation("Retrieving the ratings by carId");

            GetRatingsByCarId query = new GetRatingsByCarId()
            {
                CarId = carId
            };

            List<Rating> result = await _mediator.Send(query);

            List<GetRatingViewModel> getRatingDto = _mapper.Map<List<GetRatingViewModel>>(result);
            return Ok(getRatingDto);
        }

        /// <summary>
        /// Gets rating by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Gets a certain instance of a rating</returns>
        /// <response code="200">Gets a rating by identifier.</response>
        /// <response code="404">If the rating was not found.</response>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Log.Instance.LogInformation("Retrieving the rating by Id");

            GetRatingById query = new GetRatingById()
            {
                Id = id
            };

            Rating rating = await _mediator.Send(query);

            if (rating == null)
            {
                Log.Instance.LogException("The Id could not be found");
                return NotFound();
            }

            GetRatingViewModel getRatingDto = _mapper.Map<GetRatingViewModel>(rating);
            return Ok(getRatingDto);
        }

        /// <summary>
        /// Get a rating by carId and by renterId
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="renterId"></param>
        /// <returns>Gets a certain instance of a rating</returns>
        /// <response code="200">Gets a rating by two identifier.</response>
        /// <response code="404">If the rating was not found.</response>
        
        [HttpGet]
        [Route("Car/{carId}/Renter/{renterId}")]
        public async Task<IActionResult> GetById(int carId, int renterId)
        {
            Log.Instance.LogInformation("Retrieving the rating by carId and renterId");

            GetRatingByCarAndByRenter query = new GetRatingByCarAndByRenter()
            {
                CarId = carId,
                RenterId = renterId
            };

            Rating rating = await _mediator.Send(query);

            if (rating == null)
            {
                Log.Instance.LogException("The Id could not be found");
                return NotFound();
            }

            GetRatingViewModel getRatingDto = _mapper.Map<GetRatingViewModel>(rating);
            return Ok(getRatingDto);
        }


        /// <summary>
        /// Create a rating and adds it to the database
        /// </summary>
        /// <param name="giveRatingDto"></param>
        /// <returns>The new created object</returns> 
        /// <response code="200">Gets the created rating</response>
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GiveRatingModel giveRatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            CreateRating command = _mapper.Map<CreateRating>(giveRatingDto);
            Rating rating = await _mediator.Send(command);
            GetRatingViewModel getRatingDto = _mapper.Map<GetRatingViewModel>(rating);

            Log.Instance.LogInformation($"{rating.Rate} was given  at {DateTime.Now.TimeOfDay}");

            return CreatedAtAction(nameof(GetById), new { Id = rating.Id }, getRatingDto);
        }
    }
}
