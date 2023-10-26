using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Entities.Enums;
using Org.BouncyCastle.Asn1.Ocsp;

namespace HrELP.Presentation.Models.ViewModels
{
    public class RequestVM
    {
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public RequestType RequestType { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
        public Currency Currency { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending_Approval;
        public decimal ExpenseAmount { get; set; }
        public DateTime CreateDate { get; set; }=DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public string? FilePath { get; set; }
        public IFormFile? FormFile { get; set; }
    }
}
