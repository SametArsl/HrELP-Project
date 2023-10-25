using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(HrElpContext context) : base(context)
        {
            
        }

        public async Task<AppUser> GetUserAsync(int id)
        {
            AppUser user = await _table.Include(x => x.Address).Include(x=>x.Company).FirstOrDefaultAsync(x=>x.Id == id);
            return user;
        }

        public async Task<AppUser> GetUserWithRequestsAsync(int id)
        {
            AppUser user = await _table.Include(x => x.Company).Include(x=>x.AdvanceRequests).Include(x=>x.ExpenseRequests).Include(x=>x.LeaveRequests).FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
    }
}
