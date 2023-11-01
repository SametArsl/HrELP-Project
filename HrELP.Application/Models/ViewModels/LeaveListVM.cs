using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Models.ViewModels
{
    public class LeaveListVM
    {
        public LeaveListVM()
        {
            PendingLeaveRequest=new List<LeaveRequest>();
            ApprovedLeaveRequest=new List<LeaveRequest>();
            DeclinedLeaveRequest=new List<LeaveRequest>();
        }
        public AppUser Personnel { get; set; }
        public string? ErrorMessage { get; set; }
        public List<LeaveRequest> PendingLeaveRequest { get; set; }
        public List<LeaveRequest> ApprovedLeaveRequest { get; set; }
        public List<LeaveRequest> DeclinedLeaveRequest { get; set; }
    }
}
