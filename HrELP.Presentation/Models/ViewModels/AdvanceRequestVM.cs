using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Enums;

namespace HrELP.Presentation.Models.ViewModels
{
    public class AdvanceRequestVM
    {
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public RequestType RequestType { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
        public Currency Currency { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending_Approval;
        public decimal AdvanceAmount { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
