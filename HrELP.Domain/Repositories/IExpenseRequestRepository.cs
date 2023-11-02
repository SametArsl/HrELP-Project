using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Domain.Repositories
{
    public interface IExpenseRequestRepository : IBaseRepository<ExpenseRequest>
    {
        IQueryable<ExpenseRequest> GetAllWithAppUserAsync();
        ExpenseRequest GetById(int id);
        List<ExpenseRequest> GetPendingRequest(AppUser user);
        List<ExpenseRequest> GetAllRequests(AppUser user);
    }
}
