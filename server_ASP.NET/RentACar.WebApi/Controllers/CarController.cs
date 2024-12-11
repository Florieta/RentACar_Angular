using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using RentACar.Application.Cars.Queries;
using RentACar.Domain.Entitites;
using RentACar.WebApi.ViewModels.Car;
using RentACar.Application.Cars.Commands.Create;
using RentACar.Application.Cars.Commands.Update;
using RentACar.Application.Cars.Commands.Delete;
using AutoMapper;
using MediatR;
using RentACar.Api.Logger;
using Microsoft.AspNetCore.JsonPatch;
using RentACar.Application.Abstract;

namespace RentACar.WebApi.Controllers
{
    /// <summary>
    /// All car methods
    /// </summary>
  
    [Route("api/Car")]
    [ApiController]

    public class CarController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public CarController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all cars from the database
        /// </summary>
        /// <returns>A list of all cars</returns>
        /// <response code="200">Gets all cars.</response>

        [HttpGet]
        public async Task<IActionResult> All()
        {
            Log.Instance.LogInformation("Retrieving the list of cars");

            GetAllCars query = new GetAllCars();
            List<Car> result = await _mediator.Send(query);
            List<GetCarViewModel> mappedResult = _mapper.Map<List<GetCarViewModel>>(result);

           Log.Instance.LogInformation($"There are {result.Count} cars in the fleet");

            return Ok(mappedResult);
        }

        /// <summary>
        /// Gets latest cars from the database
        /// </summary>
        /// <returns>A list of latest cars</returns>
        /// <response code="200">Gets all cars.</response>

        [HttpGet]
        [Route("Latest")]
        public async Task<IActionResult> GetLatestCars()
        {
            Log.Instance.LogInformation("Retrieving the list of latest cars");

            GetLatestCars query = new GetLatestCars();
            List<Car> result = await _mediator.Send(query);
            List<GetCarViewModel> mappedResult = _mapper.Map<List<GetCarViewModel>>(result);

            Log.Instance.LogInformation($"There are {result.Count} cars in the fleet");

            return Ok(mappedResult);
        }

        /// <summary>
        /// Gets a certain car by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Gets a certain instance of a car</returns>
        /// <response code="200">Gets a car by identifier.</response>
        /// <response code="404">If the car was not found.</response>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Log.Instance.LogInformation("Retrieving the car by Id");

            GetCarById query = new GetCarById()
            {
                Id = id
            };

            Car car = await _mediator.Send(query);

            if (car == null)
            {
                Log.Instance.LogException("The Id could not be found");
                return NotFound();
            }

            GetCarViewModel getCarDto = _mapper.Map<GetCarViewModel>(car);
            return Ok(getCarDto);
        }

        /// <summary>
        /// Gets all cars from the database by dealerId
        /// </summary>
        /// <param name="dealerId"></param>
        /// <returns>All cars found with the specific dealerId</returns>
        /// <response code="200">Gets all cars by identifier.</response>
        
        [HttpGet]
        [Route("Dealer/{dealerId}")]

        public async Task<IActionResult> GetAllCarsByDealerId(int dealerId)
        {
            Log.Instance.LogInformation("Retrieving the list of cars");

            GetAllCarsByDealerId query = new GetAllCarsByDealerId()
            {
                DealerId = dealerId
            };
            List<Car> result = await _mediator.Send(query);
            List<GetCarViewModel> mappedResult = _mapper.Map<List<GetCarViewModel>>(result);

            Log.Instance.LogInformation($"There are {result.Count} cars in the fleet");

            return Ok(mappedResult);
        }

        /// <summary>
        /// Create a car and adds it to the database
        /// </summary>
        /// <param name="addCarDto"></param>
        /// <returns>The new created car</returns>
        /// <response code="200">Gets the created car</response>

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddCarModel addCarDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state is not valid");
            }

            CreateCar command = _mapper.Map<CreateCar>(addCarDto);
            Car car = await _mediator.Send(command);
            GetCarByIdModel getCarDto = _mapper.Map<GetCarByIdModel>(car);

            Log.Instance.LogInformation($"{car.Make} {car.Model} was created  at {DateTime.Now.TimeOfDay}");

            return CreatedAtAction(nameof(GetById), new { Id = car.Id }, getCarDto);
        }

        /// <summary>
        /// Update a car and applies the changes to the database 
        /// </summary>
        /// <param name="editCarDto"></param>
        /// <response code="200">Gets the edited car.</response>
        /// <response code="404">If the car was not found.</response>
        
        [HttpPut]

        public async Task<IActionResult> Edit([FromBody] EditCarViewModel editCarDto)
        {
            UpdateCar command = _mapper.Map<UpdateCar>(editCarDto);

            Log.Instance.LogInformation("Request with the updated car was sent!");

            Car car = await _mediator.Send(command);

            if (car == null)
            {
                return NotFound();
            }

            Log.Instance.LogInformation("The car was updated");

            return Ok(car);
        }

        /// <summary>
        /// Partial update the car and applies the changes to the database using patch
        /// </summary>
        /// <param name="jsonPatchDocument"></param>
        /// <param name="id"></param>
        /// <returns>Returns the updated object</returns>
        /// <response code="200">Gets the partial edited car</response>

        [HttpPatch]
        [Route("{Id}")]

        public async Task<IActionResult> PartialUpdate(JsonPatchDocument<EditCarViewModel> jsonPatchDocument, [FromRoute] int id)
        {
            GetCarById query = new() { Id = id };
            Car car = await _mediator.Send(query);

            EditCarViewModel editCar = _mapper.Map<EditCarViewModel>(car);

            jsonPatchDocument.ApplyTo(editCar, ModelState);

            UpdateCar command = _mapper.Map<UpdateCar>(editCar);
            command.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            car = await _mediator.Send(command);

            return Ok(car);
        }
        /// <summary>
        /// Marks the entity as deleted but it doesn't really 
        /// removes it from the database / soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <response code="204">No content</response>
        /// <response code="404">If the car was not found.</response>
     
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteCar command = new DeleteCar()
            {
                Id = id
            };

            try
            {
                Car car = await _mediator.Send(command);

                if (car == null)
                {
                    return NotFound();
                }

                Log.Instance.LogInformation($"Car with ID {id} was deleted");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
