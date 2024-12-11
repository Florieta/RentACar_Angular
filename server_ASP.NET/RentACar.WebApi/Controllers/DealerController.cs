using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Api.Logger;
using RentACar.Application.Dealers.Queries;
using RentACar.Domain.Entitites;
using RentACar.WebApi.ViewModels.Dealer;

namespace RentACar.WebApi.Controllers
{
    /// <summary>
    /// All dealer methods
    /// </summary>

    [Route("api/Dealer")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public DealerController(IMapper mapper, IMediator mediator)
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

            GetDealerByIdAsync query = new GetDealerByIdAsync()
            {
                Id = id
            };

            Dealer dealer = await _mediator.Send(query);

            if (dealer == null)
            {
                Log.Instance.LogException("The Id could not be found");
                return NotFound();
            }

            GetDealerViewModel getDealerDto = _mapper.Map<GetDealerViewModel>(dealer);
            return Ok(getDealerDto);
        }
    }
}
