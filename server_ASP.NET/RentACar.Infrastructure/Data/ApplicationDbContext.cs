using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Infrastructure.Data.Configuration;
using RentACar.Domain.Entitites;
using RentACar.Domain.Entitites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Data
{
    /// <summary>
    /// Application Database Context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="options">Context options</param>
        /// 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Orders { get; init; } = null!;

        public DbSet<Car> Cars { get; init; } = null!;

        public DbSet<Category> Categories { get; init; } = null!;

        public DbSet<Location> Locations { get; init; } = null!;

        public DbSet<Renter> Renters { get; init; } = null!;

        public DbSet<Dealer> Dealers { get; init; } = null!;

        public DbSet<Rating> Ratings { get; init; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new CarConfiguration());
            builder.ApplyConfiguration(new LocationConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new RenterConfiguration());
            builder.ApplyConfiguration(new DealerConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
