using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Api.Logger;
using RentACar.Application.Categories.Commands.Create;
using RentACar.Application.Categories.Queries;
using RentACar.Application.Orders.Queries;
using RentACar.Domain.Entitites;
using RentACar.WebApi.Middleware;
using RentACar.WebApi.ViewModels.Category;

namespace RentACar.WebApi.Controllers
{
    /// <summary>
    /// All category methods 
    /// </summary>
    
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public CategoryController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets all categories from the database
        /// </summary>
        /// <returns>A list of all categories</returns>
        /// <response code="200">Gets all cars.</response>
        
        [HttpGet]
        public async Task<IActionResult> All()
        {
            Log.Instance.LogInformation("Retrieving the list of categories");

            GetAllCategories query = new GetAllCategories();
            List<Category> result = await _mediator.Send(query);
            List<GetCategoryViewModel> mappedResult = _mapper.Map<List<GetCategoryViewModel>>(result);

            Log.Instance.LogInformation($"There are {result.Count} categories in the fleet");

            return Ok(mappedResult);
        }

        /// <summary>
        /// Get a certain category by Id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Gets a certain category by indentifier</returns>
        /// <response code="200">Gets a category by identifier.</response>
        /// <response code="404">If the category was not found.</response>
        
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Log.Instance.LogInformation("Retrieving the category by Id");

            GetCategoryById query = new GetCategoryById()
            {
                Id = id
            };

            Category category = await _mediator.Send(query);

            if (category == null)
            {
                Log.Instance.LogWarning("The Id could not be found");
                return NotFound();
            }

            GetCategoryViewModel getCategoryDto = _mapper.Map<GetCategoryViewModel>(category);
            return Ok(getCategoryDto);
        }

        /// <summary>
        /// Create a category and adds it to the database
        /// </summary>
        /// <param name="addCategoryDto"></param>
        /// <returns>The new created category</returns>
        /// <response code="200">Gets the created car</response>
        
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] AddCategoryModel addCategoryDto)
        {
            CreateCategory command = _mapper.Map<CreateCategory>(addCategoryDto);
            Category category = await _mediator.Send(command);
            GetCategoryViewModel getCategoryDto = _mapper.Map<GetCategoryViewModel>(category);

            Log.Instance.LogInformation($"{category.CategoryName} was created  at {DateTime.Now.TimeOfDay}");

            return CreatedAtAction(nameof(GetById), new { Id = category.Id }, getCategoryDto);
        }

    }
}
