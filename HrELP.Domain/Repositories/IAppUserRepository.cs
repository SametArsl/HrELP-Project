using HrELP.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Domain.Repositories
{
    public interface IAppUserRepository : IBaseRepository<AppUser>
    {
        Task<AppUser> GetUserAsync(int id);
        Task<AppUser> GetUserWithRequestsAsync(int id);
    }
}
