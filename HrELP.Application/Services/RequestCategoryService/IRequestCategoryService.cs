using HrELP.Domain.Entities.Concrete.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.RequestCategoryService
{
    public interface IRequestCategoryService
    {
        Task<RequestCategory> GetCategoryById(int id);
    }
}
