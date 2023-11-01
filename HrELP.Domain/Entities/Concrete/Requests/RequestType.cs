using HrELP.Domain.Entities.Abstract;

namespace HrELP.Domain.Entities.Concrete.Requests
{
    public class RequestType : IBaseEntity
    {
        public RequestType()
        {
            AdvanceRequests = new HashSet<AdvanceRequest>();
            ExpenseRequests = new HashSet<ExpenseRequest>();
            LeaveRequests = new HashSet<LeaveRequest>();
          
        }
        public int Id { get; set; }
        public string RequestName { get; set; }
        public int? RequestCategoryId { get; set; }
        public RequestCategory RequestCategory { get; set; }
        public ICollection<AdvanceRequest>? AdvanceRequests { get; set; }
        public ICollection<ExpenseRequest>? ExpenseRequests { get; set; }  
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }  
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}