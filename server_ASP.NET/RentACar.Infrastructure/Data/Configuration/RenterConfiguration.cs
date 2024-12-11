using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Data.Configuration
{
    public class RenterConfiguration : IEntityTypeConfiguration<Renter>
    {

        public void Configure(EntityTypeBuilder<Renter> builder)
        {
            builder.HasData(CreateRenters());

            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.ApplicationUser)
                .WithOne(d => d.Renter);
            builder.Property(c => c.Age)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(c => c.DrivingLicenceNumber)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(c => c.ExpiredDate)
                .IsRequired();
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(75);
        }

        private List<Renter> CreateRenters()
        {
            var renters = new List<Renter>()
            {
                new Renter()
                 {
                      Id = 1,
                      Age = 26,
                      DrivingLicenceNumber = "12345674",
                      Address = "Sofia, Bulgaria, Mladost 3",
                      ExpiredDate = new DateTime(2025, 11, 17, 0, 0, 0),
                      ApplicationUserId = "c6e570fd-d889-4a67-a36a-0ecbe758bc2c"
                 },

            };

            return renters;
        }
    }
}
