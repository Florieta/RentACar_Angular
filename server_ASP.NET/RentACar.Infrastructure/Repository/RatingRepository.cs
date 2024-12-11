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
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task AddAsync(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
        }

        /// <inheritdoc />
        public async Task<List<Rating>> GetAllAsync()
        {
            return await _context.Ratings
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<Rating>> GetAllAsyncByCarId(int carId)
        {
            return await _context.Ratings.Where(x => x.CarId == carId)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Rating> GetByIdAsync(int ratingId)
        {
            var rating = await _context.Ratings
                .FirstOrDefaultAsync(p => p.Id == ratingId);

            return rating;
        }

        /// <inheritdoc />
        public async Task<Rating> GetByCarIdAndRenterIdAsync(int carId, int renterId )
        {
            var rating = await _context.Ratings
                .FirstOrDefaultAsync(p => p.CarId == carId && p.RenterId == renterId);

            return rating;
        }

    }
}
