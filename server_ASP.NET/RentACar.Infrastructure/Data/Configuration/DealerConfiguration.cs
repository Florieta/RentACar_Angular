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
    public class DealerConfiguration : IEntityTypeConfiguration<Dealer>
    {

        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            builder.HasData(CreateDealers());

            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.ApplicationUser)
                .WithOne(d => d.Dealer);
            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(c => c.CompanyNumber)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(75);
        }

        private List<Dealer> CreateDealers()
        {
            var dealers = new List<Dealer>()
            {
                new Dealer()
                 {
                      Id = 1,
                      CompanyName = "TopCars",
                      CompanyNumber = "12345674",
                      Address = "Sofia, Bulgaria, 1000, West Park",
                      ApplicationUserId = "d3211a8d-efde-4a19-8087-79cde4679276"
                 },

            };

            return dealers;
        }
    }
}
