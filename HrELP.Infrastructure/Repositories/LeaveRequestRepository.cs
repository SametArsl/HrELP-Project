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
    public class LeaveRequestRepository : BaseRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrElpContext context) : base(context)
        {
        }

        public LeaveRequest GetById(int id)
        {
            LeaveRequest? leaveRequest = _table.Include(x => x.RequestType).ThenInclude(x => x.RequestCategory).Include(x => x.AppUser).FirstOrDefault(x => x.Id == id);
            return leaveRequest;
        }

        IQueryable<LeaveRequest> ILeaveRequestRepository.GetAllWithAppUserAsync()
        {
            IQueryable<LeaveRequest> list = _table.Include(x => x.AppUser).Include(y => y.RequestType);

            return list;
        }
    }
}
