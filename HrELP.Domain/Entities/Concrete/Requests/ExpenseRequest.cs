using HrELP.Domain.Entities.Abstract;
using HrELP.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Domain.Entities.Concrete.Requests
{
    public class ExpenseRequest : IBaseEntity, IRequest
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsActive { get; set; }
        public decimal ExpenseAmount { get; set; }
        public Currency Currency { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? RequestTypeId { get; set; }
        public RequestType? RequestType { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending_Approval;
        public DateTime? ResponseDate { get; set; }
        public string? FilePath { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
