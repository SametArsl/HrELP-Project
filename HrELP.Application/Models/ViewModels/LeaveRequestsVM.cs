using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Models.ViewModels
{
    public class LeaveRequestsVM
    {
        [Required]
        public int PersonnelId { get; set; }
        [Required]
        public AppUser Personnel { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int LeaveTypeId { get; set; }
        [Required]
        public List<LeaveTypes> LeaveTypes { get; set; }
        [Required]
        public List<LeaveRequest> pendingLeaveList { get; set; }
    }
}
