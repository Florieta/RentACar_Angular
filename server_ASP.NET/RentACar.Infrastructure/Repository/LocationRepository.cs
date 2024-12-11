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
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task AddAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
        }

        /// <inheritdoc />
        public async Task<List<Location>> GetAllAsync()
        {
            return await _context.Locations
                .Where(l => l.IsDeleted == false)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Location> GetByIdAsync(int locationId)
        {
            var location = await _context.Locations
                .FirstOrDefaultAsync(p => p.Id == locationId);

            return location;
        }

    }
}
