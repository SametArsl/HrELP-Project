using HrELP.Domain.Entities.Concrete.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Domain.Repositories
{
    public interface ILeaveRequestRepository : IBaseRepository<LeaveRequest>
    {
        IQueryable<LeaveRequest> GetAllWithAppUserAsync();
        LeaveRequest GetById(int id);
    }
}
