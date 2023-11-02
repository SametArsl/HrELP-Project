using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.ExpenseRequestService
{
    public interface IExpenseRequestService
    {
        List<ExpenseRequest> GetAll();
        Task CreateRequest(ExpenseRequest request);
        Task<ExpenseRequest> GetRequestById(int id);
        Task UpdateAsync(ExpenseRequest request);
        List<ExpenseRequest> PendingRequests(AppUser user);
        List<ExpenseRequest> AllRequests(AppUser user);
        Task DeleteAsync(ExpenseRequest request);
    }
}
