using AutoMapper;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.RequestCategoryService
{
    public class RequestCategoryService : IRequestCategoryService
    {
        private readonly IRequestCategoryRepository _categoryRepository;

        public RequestCategoryService(IRequestCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<RequestCategory> GetCategoryById(int id)
        {
            return _categoryRepository.GetFirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
