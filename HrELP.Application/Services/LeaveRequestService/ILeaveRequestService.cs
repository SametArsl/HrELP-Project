using HrELP.Domain.Entities.Concrete.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.LeaveRequestService
{
    public interface ILeaveRequestService
    {
        List<LeaveRequest> GetAll();
        Task CreateRequest(LeaveRequest request);
        Task<LeaveRequest> GetRequestById(int id);
    }
}
