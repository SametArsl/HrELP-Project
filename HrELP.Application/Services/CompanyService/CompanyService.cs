using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly SignInManager<AppUser> _signInManager;

        public CompanyService(ICompanyRepository companyRepository, SignInManager<AppUser> signInManager)
        {
            _companyRepository = companyRepository;
            _signInManager = signInManager;
        }

        public async Task<Company> GetCompany(int id)
        {
            Company company = await _companyRepository.GetFirstOrDefaultAsync(x => x.CompanyId == id);
            return company;
        }
    }
}
