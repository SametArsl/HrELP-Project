using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Enums;

namespace HrELP.Presentation.Models.ViewModels
{
    public class LeaveRequestVM
    {
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public RequestType RequestTypes { get; set; }
        public LeaveTypes LeaveType { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending_Approval;
        public decimal TotalDaysOff { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
