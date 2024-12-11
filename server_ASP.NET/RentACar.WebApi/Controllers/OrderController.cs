using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Api.Logger;
using RentACar.Application.Orders.Commands.Create;
using RentACar.Application.Orders.Commands.Delete;
using RentACar.Application.Orders.Queries;
using RentACar.Domain.Entitites;
using RentACar.WebApi.Middleware;
using RentACar.WebApi.ViewModels.Order;

namespace RentACar.WebApi.Controllers
{
    /// <summary>
    /// All order methods
    /// </summary>
    
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public OrderController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all orders from the database
        /// </summary>
        /// <returns>A list of all orders</returns>
        /// <response code="200">Gets all orders.</response>

        [HttpGet]
        public async Task<IActionResult> All()
        {
            Log.Instance.LogInformation("Retrieving the list of bookings");

            GetAllOrders query = new GetAllOrders();
            List<Order> result = await _mediator.Send(query);
            List<GetOrderViewModel> mappedResult = _mapper.Map<List<GetOrderViewModel>>(result);

            Log.Instance.LogInformation($"There are {result.Count} bookings in the fleet");

            return Ok(mappedResult);
        }

        /// <summary>
        /// Gets a certain order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A certain instance of order</returns>
        /// <response code="200">Gets an order by identifier.</response>
        /// <response code="404">If the order was not found.</response>
        
        [HttpGet("{Id}")]

        public async Task<IActionResult> GetById(int id)
        {
            Log.Instance.LogInformation("Retrieving the booking by Id");

            GetOrderById query = new GetOrderById()
            {
                Id = id
            };

            Order order = await _mediator.Send(query);

            if (order == null)
            {
                Log.Instance.LogWarning("The Id could not be found");
                return NotFound();
            }

            GetOrderViewModel getOrderDto = _mapper.Map<GetOrderViewModel>(order);
            return Ok(getOrderDto);
        }

        /// <summary>
        /// Get all orders by renterId from the database
        /// </summary>
        /// <param name="renterId"></param>
        /// <returns>A list of all orders by renterId</returns>
        /// <response code="200">Gets all oders by identifier.</response>
        

        [HttpGet]
        [Route("Renter/{renterId}")]

        public async Task<IActionResult> GetAllBookingsByRenter(int renterId)
        {
            Log.Instance.LogInformation("Retrieving the list of bookings");

            GetAllCarsByDealerId query = new GetAllCarsByDealerId()
            {
                RenterId = renterId
            };
            List<Order> result = await _mediator.Send(query);
            List<GetOrderViewModel> mappedResult = _mapper.Map<List<GetOrderViewModel>>(result);

            Log.Instance.LogInformation($"There are {result.Count} bookings in the fleet");

            return Ok(mappedResult);
        }

        /// <summary>
        /// Create a order and adds it to the database
        /// </summary>
        /// <param name="addOrderDto"></param>
        /// <returns>The new created order</returns>
        /// <response code="200">Gets the created order</response>
        
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] AddOrderModel addOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state is not valid");
            }
            CreateOrder command = _mapper.Map<CreateOrder>(addOrderDto);
            Order order = await _mediator.Send(command);
            GetOrderViewModel getOrderDto = _mapper.Map<GetOrderViewModel>(order);

            Log.Instance.LogInformation($"A new booking was created  at {DateTime.Now.TimeOfDay}");

            return CreatedAtAction(nameof(GetById), new { Id = order.Id }, getOrderDto);
        }

        /// <summary>
        /// Marks the entity as deleted but it doesn't really 
        /// removes it from the database / soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <response code="204">No content</response>
        /// <response code="404">If the order was not found.</response>
        
        [HttpDelete("{Id}")]

        public async Task<IActionResult> Delete(int id)
        {
            DeleteOrder command = new() { Id = id };

            try
            {
                Order order = await _mediator.Send(command);
                Log.Instance.LogInformation("A booking was deleted from the list");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
