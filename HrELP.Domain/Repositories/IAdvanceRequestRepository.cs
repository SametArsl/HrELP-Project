using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Domain.Repositories
{
    public interface IAdvanceRequestRepository : IBaseRepository<AdvanceRequest>
    {
        IQueryable<AdvanceRequest> GetAllWithAppUserAsync();
        AdvanceRequest GetById(int id);
        List<AdvanceRequest> GetPendingRequest(AppUser user);
        List<AdvanceRequest> GetAllRequests(AppUser user);
    }
}
