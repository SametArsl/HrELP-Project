using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.LeaveRequestService
{
    public interface ILeaveRequestService
    {
        Task CreateLeaveRequestAsync(LeaveRequest request);
        Task<LeaveRequest> GetLeaveRequestAsync(int leaveRequestId);
        Task<List<LeaveRequest>> GetLeaveRequestAsync();
        Task<List<LeaveRequest>> GetLeaveRequestByStatusAsync( int id, params Expression<Func<LeaveRequest, object>>[] includes);
        Task<List<LeaveRequest>> GetLeaveRequestByStatusAsync(ApprovalStatus approvalStatus,int id,params Expression<Func<LeaveRequest, object>>[] includes);
        Task DeniedLeaveRequestAsync(int id);
        List<LeaveRequest> GetAll();
        Task CreateRequest(LeaveRequest request);
        Task<LeaveRequest> GetRequestById(int id);
        Task UpdateAsync(LeaveRequest request);
        Task DeleteAsync(LeaveRequest request);
    }
}
