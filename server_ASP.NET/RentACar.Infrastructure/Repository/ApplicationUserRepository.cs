using Microsoft.EntityFrameworkCore;
using RentACar.Application.Abstract;
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
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<ApplicationUser> GetByIdAsync(string applicationUserId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(p => p.Id == applicationUserId);

            return user;
        }
        /// <inheritdoc />
        public async Task Update(ApplicationUser user)
        {
            _context.Update(user);
        }
    }
}
