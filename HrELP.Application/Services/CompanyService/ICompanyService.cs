using HrELP.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<Company> GetCompany(int id);
    }
}
