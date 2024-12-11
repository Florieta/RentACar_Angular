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
    public class DealerRepository : IDealerRepository
    {
        private readonly ApplicationDbContext _context;

        public DealerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task AddAsync(Dealer dealer)
        {
            await _context.Dealers.AddAsync(dealer);
        }

        /// <inheritdoc />
        public async Task<List<Dealer>> GetAllAsync()
        {
            return await _context.Dealers
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Dealer> GetByIdAsync(int dealerId)
        {
            var dealer = await _context.Dealers.Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(p => p.Id == dealerId);

            return dealer;
        }
        /// <inheritdoc />
        public async Task Update(Dealer dealer)
        {
            _context.Update(dealer);
        }
    }
}
