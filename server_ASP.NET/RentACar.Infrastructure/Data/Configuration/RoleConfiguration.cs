﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole()
            {
                Id = "d86dba5034324ec481562264fecc1d3b",
                Name = "Dealer",
                NormalizedName = "DEALER"
            });

            builder.HasData(new IdentityRole()
            {
                Id = "5af4facac8424694b91c57854ab6b598",
                Name = "Renter",
                NormalizedName = "RENTER"
            });
        }
    }
}
