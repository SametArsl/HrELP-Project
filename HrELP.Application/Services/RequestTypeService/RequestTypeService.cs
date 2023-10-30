using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.RequestTypeService
{
    public class RequestTypeService : IRequestTypeService
    {
        private readonly IRequestTypeRepository _repository;

        public RequestTypeService(IRequestTypeRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<RequestType> GetExpenseRequestTypes()
        {
            return _repository.GetAll().Include(x => x.RequestCategory).Where(x=>x.RequestCategoryId==5);
        }
        public async Task<RequestType> GetTypeById(int id)
        {
            return await _repository.GetFirstOrDefaultAsync(x => x.Id == id);
        }
        public IQueryable<RequestType> GetAdvanceRequestTypes()
        {
            return _repository.GetAll().Include(x=>x.RequestCategory).Where(x=>x.RequestCategoryId==6);
        }
        public IQueryable<RequestType> GetLeaveRequestTypes()
        {
            return _repository.GetAll().Include(x => x.RequestCategory).Where(x => x.RequestCategoryId == 7);
        }
    }
}
