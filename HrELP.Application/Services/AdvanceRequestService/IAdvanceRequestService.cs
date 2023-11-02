using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.AdvanceRequestService
{
    public interface IAdvanceRequestService
    {
        List<AdvanceRequest> GetAll();
        Task CreateRequest(AdvanceRequest request);
        Task<AdvanceRequest> GetRequestById(int id);
        Task UpdateAsync(AdvanceRequest request);
        List<AdvanceRequest> PendingRequests(AppUser user);
        List<AdvanceRequest> AllRequests(AppUser user);
        Task DeleteAsync(AdvanceRequest request);
    }
}
