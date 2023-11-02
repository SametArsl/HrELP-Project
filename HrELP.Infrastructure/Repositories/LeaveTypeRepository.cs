using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.Repositories
{
    public class LeaveTypeRepository : BaseRepository<LeaveTypes>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrElpContext context) : base(context)
        {

        }
        public async Task<LeaveTypes> GetLeaveTypeById(int id)
        {
            LeaveTypes leaveType = await _table.FirstAsync(x => x.LeaveTypeId == id);
            return leaveType;
        }
    }
}
