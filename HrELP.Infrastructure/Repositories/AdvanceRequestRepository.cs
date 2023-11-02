using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.Repositories
{
    public class AdvanceRequestRepository : BaseRepository<AdvanceRequest>, IAdvanceRequestRepository
    {
        public AdvanceRequestRepository(HrElpContext context) : base(context)
        {
        }

        public AdvanceRequest GetById(int id)
        {
            AdvanceRequest? advanceRequest = _table.Include(x => x.RequestType).ThenInclude(x => x.RequestCategory).Include(x => x.AppUser).FirstOrDefault(x => x.Id == id);
            return advanceRequest;
        }

        public List<AdvanceRequest> GetPendingRequest(AppUser user)
        {
            List<AdvanceRequest> list = _table.Include(x => x.AppUser).Include(x => x.RequestType).Where(x => x.UserId == user.Id && x.ApprovalStatus==Domain.Entities.Enums.ApprovalStatus.Pending_Approval).ToList();
            return list;
        }

        public List<AdvanceRequest> GetAllRequests(AppUser user)
        {
            List<AdvanceRequest> list = _table.Include(x => x.AppUser).Include(x => x.RequestType).Where(x => x.UserId == user.Id).ToList();
            return list;
        }

        IQueryable<AdvanceRequest> IAdvanceRequestRepository.GetAllWithAppUserAsync()
        {
            IQueryable<AdvanceRequest> list = _table.Include(x => x.AppUser).Include(y => y.RequestType);

            return list;
        }
    }
}
