using Microsoft.EntityFrameworkCore;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using RentACar.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repository
{
    /// <inheritdoc />
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <inheritdoc />
        public async Task AddAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
        }
        /// <inheritdoc />
        public async Task<List<Car>> GetAllAsync()
        {
            return await _context.Cars
                .Where(c => c.IsDeleted == false && c.IsAvailable == true)
                .Include(c => c.Category)
                .ToListAsync();
        }
        /// <inheritdoc />
        public async Task<List<Car>> GetCarsByDealerIdAsync(int dealerId)
        {
            return await _context.Cars
                .Where(c => c.IsDeleted == false && c.DealerId == dealerId)
                .Include(c => c.Category)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<Car>> GetLatestCarsAsync()
        {
            return await _context.Cars
                .Where(c => c.IsDeleted == false && c.IsAvailable == true)
                .Include(c => c.Category).Take(6).OrderByDescending(x => x.Id)
                .ToListAsync();
        }
        /// <inheritdoc />
        public async Task<Car> GetByIdAsync(int carId)
        {
            var car = await _context.Cars.Include(x => x.Category).AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == carId && p.IsDeleted == false);

            return car;
        }

        /// <inheritdoc />
        public async Task<Car> GetByIdAsyncWithTracking(int carId)
        {
            var car = await _context.Cars.Include(x => x.Category)
                .FirstOrDefaultAsync(p => p.Id == carId && p.IsDeleted == false);

            return car;
        }
        /// <inheritdoc />
        public void Remove(Car car)
        {
            if (!car.IsDeleted)
            {
                car.IsDeleted = true;
            }
        }
        /// <inheritdoc />
        public async Task Update(Car car)
        {
          _context.Update(car);
        }
    }
}
