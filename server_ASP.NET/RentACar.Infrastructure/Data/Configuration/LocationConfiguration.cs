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
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {

        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasData(CreateLocations());
            builder.HasKey(e => e.Id);

            builder.Property(l => l.LocationName)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(100);
       
        }
        private List<Location> CreateLocations()
        {
            var locations = new List<Location>()
            {
                new Location()
                 {
                      Id = 1,
                      LocationName = "Varna Center",
                      Address = "Bulgaria, Varna, 9000"
                 },

                new Location()
                {
                    Id = 2,
                    LocationName = "Varna Airport",
                    Address = "Bulgaria, Varna, 9000"
                },

                new Location()
                {
                    Id = 3,
                    LocationName = "Sofia Airport",
                    Address = "Bulgaria, Sofia, 1000"

                },

                 new Location()
                {
                    Id = 4,
                    LocationName = "Sofia Center",
                    Address = "Bulgaria, Sofia, 1000"

                },
                 new Location()
                {
                    Id = 5,
                    LocationName = "Burgas Airport",
                    Address = "Bulgaria, Burgas"

                }


            };

            return locations;
        }
    }
}
