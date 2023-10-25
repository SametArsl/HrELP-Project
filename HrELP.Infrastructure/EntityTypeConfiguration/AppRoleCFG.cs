using HrELP.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.EntityTypeConfiguration
{
    public class AppRoleCFG: BaseEntityConfiguration<AppRole>
    {
        public override void Configure(EntityTypeBuilder<AppRole> builder)
        {

            builder.HasData(
               new AppRole { Id = 1, Name = "Manager", NormalizedName = "MANAGER" },
               new AppRole { Id = 2, Name = "Admin", NormalizedName = "ADMIN" },
               new AppRole { Id = 3, Name = "Personnel", NormalizedName = "PERSONNEL" }
           );

            base.Configure(builder);
        }
    }
}
