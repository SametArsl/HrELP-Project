using HrELP.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.EntityTypeConfiguration
{
    public class CompanyCFG: BaseEntityConfiguration<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasMany(x => x.AppUsers).WithOne(x => x.Company);
            builder.HasData(new Company()
            {
                CompanyId=1,
                CompanyName = "BilgeAdam Boost",
                IsActive = true
            });
            base.Configure(builder);
        }
    }
}
