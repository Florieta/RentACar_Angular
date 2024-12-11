using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Api.Logger;
using RentACar.Application.Renters.Queries;
using RentACar.Domain.Entitites;
using RentACar.WebApi.ViewModels.Renter;

namespace RentACar.WebApi.Controllers
{
    /// <summary>
    /// All renter methods
    /// </summary>

    [Route("api/Renter")]
    [ApiController]
    public class RenterController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public RenterController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a certain renter by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Gets a certain instance of a renter</returns>
        /// <response code="200">Gets a renter by identifier.</response>
        /// <response code="404">If the renter was not found.</response>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Log.Instance.LogInformation("Retrieving the renter by Id");

            GetRenterById query = new GetRenterById()
            {
                Id = id
            };

            Renter renter = await _mediator.Send(query);

            if (renter == null)
            {
                Log.Instance.LogException("The Id could not be found");
                return NotFound();
            }

            GetRenterViewModel getRenterDto = _mapper.Map<GetRenterViewModel>(renter);
            return Ok(getRenterDto);
        }
    }
}
