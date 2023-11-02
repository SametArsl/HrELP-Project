using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.EntityTypeConfiguration
{
    public class LeaveTypeCFG : BaseEntityConfiguration<LeaveTypes>
    {
        public override void Configure(EntityTypeBuilder<LeaveTypes> builder)
        {
            builder.HasKey(x => x.LeaveTypeId);
            builder.Property(x => x.LeaveTypeName).IsRequired().HasMaxLength(150);
            builder.HasData(
                new LeaveTypes { LeaveTypeId = 1, LeaveTypeName = "Pregnancy Leave", Gender = Domain.Entities.Enums.Gender.Female, DayValue = 60 },
                new LeaveTypes { LeaveTypeId = 2, LeaveTypeName = "Paternity Leave", Gender = Domain.Entities.Enums.Gender.Male, DayValue = 5 },
                new LeaveTypes { LeaveTypeId = 3, LeaveTypeName = "Sick Leave", Gender = Domain.Entities.Enums.Gender.Female, DayValue = 5 },
                new LeaveTypes { LeaveTypeId = 4, LeaveTypeName = "Sick Leave", Gender = Domain.Entities.Enums.Gender.Male, DayValue = 5 },
                new LeaveTypes { LeaveTypeId = 5, LeaveTypeName = "Funeral Leave", Gender = Domain.Entities.Enums.Gender.Male, DayValue = 3 },
                new LeaveTypes { LeaveTypeId = 6, LeaveTypeName = "Funeral Leave", Gender = Domain.Entities.Enums.Gender.Female, DayValue = 3 },
                new LeaveTypes { LeaveTypeId = 7, LeaveTypeName = "Marriage Leave", Gender = Domain.Entities.Enums.Gender.Male, DayValue = 10 },
                new LeaveTypes { LeaveTypeId = 8, LeaveTypeName = "Marriage Leave", Gender = Domain.Entities.Enums.Gender.Female, DayValue = 10 },
                new LeaveTypes { LeaveTypeId = 9, LeaveTypeName = "Pregnancy Control Leave", Gender = Domain.Entities.Enums.Gender.Female, DayValue = 1 },
                new LeaveTypes { LeaveTypeId = 10, LeaveTypeName = "Unpaid Leave", Gender = Domain.Entities.Enums.Gender.Female, DayValue = 1 },
                new LeaveTypes { LeaveTypeId = 11, LeaveTypeName = "Unpaid Leave", Gender = Domain.Entities.Enums.Gender.Male, DayValue = 1 },
                new LeaveTypes { LeaveTypeId = 12, LeaveTypeName = "Patient Relative Leave", Gender = Domain.Entities.Enums.Gender.Male, DayValue = 1 },
                new LeaveTypes { LeaveTypeId = 13, LeaveTypeName = "Patient Relative Leave", Gender = Domain.Entities.Enums.Gender.Female, DayValue = 1 },
                new LeaveTypes { LeaveTypeId = 14, LeaveTypeName = "Milk Leave", DayValue = 40, Gender = Domain.Entities.Enums.Gender.Female },
                new LeaveTypes { LeaveTypeId = 15, LeaveTypeName = "New Job Leave", DayValue = 0.5M, Gender = Domain.Entities.Enums.Gender.Female },
                new LeaveTypes { LeaveTypeId = 16, LeaveTypeName = "New Job Leave", DayValue = 0.5M, Gender = Domain.Entities.Enums.Gender.Male },
                 new LeaveTypes { LeaveTypeId = 17, LeaveTypeName = "Adoption Leave", DayValue = 40, MarriageStatus = true, Gender = Domain.Entities.Enums.Gender.Male },
                  new LeaveTypes { LeaveTypeId = 18, LeaveTypeName = "Adoption Leave", DayValue = 40, MarriageStatus = true, Gender=Domain.Entities.Enums.Gender.Female }


                );
            base.Configure(builder);
        }
    }
}
