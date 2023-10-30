using HrELP.Domain.Entities.Concrete.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.RequestTypeService
{
    public interface IRequestTypeService
    {
        IQueryable<RequestType> GetExpenseRequestTypes();
        Task<RequestType> GetTypeById(int id);
        IQueryable<RequestType> GetAdvanceRequestTypes();
        IQueryable<RequestType> GetLeaveRequestTypes();
    }
}
