using HrELP.Domain.Entities.Concrete.Requests;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.EntityTypeConfiguration
{
    public class RequestTypeCFG: BaseEntityConfiguration<RequestType>
    {
        public override void Configure(EntityTypeBuilder<RequestType> builder)
        {
            builder.HasOne(x=>x.RequestCategory).WithMany(x=>x.RequestTypes).HasForeignKey(x=>x.RequestCategoryId);
            builder.HasMany(x=>x.ExpenseRequests).WithOne(x=>x.RequestType).HasForeignKey(x=>x.RequestTypeId);
            builder.HasMany(x=>x.AdvanceRequests).WithOne(x=>x.RequestType).HasForeignKey(x=>x.RequestTypeId);
            builder.HasMany(x=>x.LeaveRequests).WithOne(x=>x.RequestType).HasForeignKey(x=>x.RequestTypeId);
            base.Configure(builder);
        }
    }
}
