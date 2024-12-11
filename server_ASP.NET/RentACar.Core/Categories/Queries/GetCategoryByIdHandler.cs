using MediatR;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Categories.Queries
{
    /// <summary>
    /// Get category by id handler
    /// </summary>
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, Category>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public GetCategoryByIdHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Get a category by identifier
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a category</returns>
        public async Task<Category> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            return await this.unitOfWorkRepo.CategoryRepository.GetByIdAsync(request.Id);
        }
    }
}
