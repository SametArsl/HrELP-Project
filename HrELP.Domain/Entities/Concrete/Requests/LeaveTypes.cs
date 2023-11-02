using HrELP.Domain.Entities.Abstract;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Domain.Entities.Concrete
{
    public class LeaveTypes : IBaseEntity
    {
        public LeaveTypes()
        {
              LeaveRequests = new HashSet<LeaveRequest>();
        }
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public decimal DayValue { get; set; }
        public Gender Gender { get; set; }
        public bool? MarriageStatus { get; set; }
        public bool IsActive { get ; set ; }
        public DateTime? CreateDate { get ; set ; }
        public DateTime? UpdateDate { get ; set ; }
        public ICollection<LeaveRequest>? LeaveRequests { get ; set ; }

    }
}
