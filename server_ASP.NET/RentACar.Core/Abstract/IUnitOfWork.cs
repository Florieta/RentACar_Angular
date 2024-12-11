using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Abstract
{
    /// <summary>
    /// Implements Unit of Work pattern containing all repositories
    /// </summary>
    public interface IUnitOfWork
    {
        public ICarRepository CarRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        public IOrderRepository OrderRepository { get; }

        public ILocationRepository LocationRepository { get; }
        public IRatingRepository RatingRepository { get; }
        public IDealerRepository DealerRepository { get; }
        public IRenterRepository RenterRepository { get; }

        Task SaveAsync();
    }
}
