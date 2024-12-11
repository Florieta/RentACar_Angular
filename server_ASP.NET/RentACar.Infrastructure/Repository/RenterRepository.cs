using Microsoft.EntityFrameworkCore;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using RentACar.Domain.Entitites.Identity;
using RentACar.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repository
{
    /// <inheritdoc />
    public class RenterRepository : IRenterRepository
    {
        private readonly ApplicationDbContext _context;

        public RenterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task AddAsync(Renter renter)
        {
            await _context.Renters.AddAsync(renter);
        }

        /// <inheritdoc />
        public async Task<List<Renter>> GetAllAsync()
        {
            return await _context.Renters
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Renter> GetByIdAsync(int renterId)
        {
            var renter = await _context.Renters.Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(p => p.Id == renterId);

            return renter;
        }

        /// <inheritdoc />
        public async Task Update(Renter renter)
        {
            _context.Update(renter);
        }

    }
}
