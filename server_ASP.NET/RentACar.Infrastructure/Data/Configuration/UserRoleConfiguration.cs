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
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
             new IdentityUserRole<string>()
             {
                 RoleId = "d86dba5034324ec481562264fecc1d3b",
                 UserId = "d3211a8d-efde-4a19-8087-79cde4679276"
             });

            builder.HasData(
             new IdentityUserRole<string>()
             {
                 RoleId = "5af4facac8424694b91c57854ab6b598",
                 UserId = "c6e570fd-d889-4a67-a36a-0ecbe758bc2c"
             });
        }
    }
}
