using AutoMapper;
using Azure.Storage.Blobs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Api.Logger;
using RentACar.Application.Abstract;
using RentACar.Application.Cars.Queries;
using RentACar.Domain.Entitites;
using RentACar.Domain.Models;
using RentACar.Infrastructure.Data;

namespace RentACar.WebApi.Controllers
{
    /// <summary>
    /// Blob storage methods
    /// </summary>

    [Route("api/BlobStorage")]
    [ApiController]
    public class BlobStorageController : ControllerBase
    {
        private readonly IStorageService _storageService;
        public readonly IMapper _mapper;
        public BlobStorageController(IStorageService storageService, IMapper mapper)
        {
            _storageService = storageService;
            _mapper = mapper;
        }
        /// <summary>
        /// Upload a file in the storage
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ok</returns>
        /// <response code="200">The file was uploaded.</response>

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] FileModel model)
        { 
            if (model.ImageFile != null)
            {
                await _storageService.Upload(model);
            }
            return Ok();
        }

    }
}
