using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Entities.Enums;
using Org.BouncyCastle.Asn1.Ocsp;

namespace HrELP.Presentation.Models.ViewModels
{
    public class RequestVM
    {
        public int Id { get; set; }
        public string RequestCategoryId { get; set; }
        public RequestCategory RequestCategory { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
        public Currency Currency { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending_Approval;
        public decimal ExpenseAmount { get; set; }
    }
}
