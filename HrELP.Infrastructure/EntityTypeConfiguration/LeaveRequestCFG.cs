using HrELP.Domain.Entities.Concrete.Requests;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.EntityTypeConfiguration
{
    public class LeaveRequestCFG:BaseEntityConfiguration<LeaveRequest>
    {
        public override void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(x=>x.LeaveType).WithMany(x=>x.LeaveRequests).HasForeignKey(x=>x.LeaveTypeId);
            builder.HasOne(x=>x.AppUser).WithMany(x=>x.LeaveRequests).HasForeignKey(x=>x.UserId);
            base.Configure(builder);
        }
    }
}
