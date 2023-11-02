using AutoMapper;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
using HrELP.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.ExpenseRequestService
{
    public class ExpenseRequestService : IExpenseRequestService
    {
        private readonly IExpenseRequestRepository _expenseRequestRepository;

        public ExpenseRequestService(IExpenseRequestRepository expenseRequestRepository)
        {
            _expenseRequestRepository = expenseRequestRepository;
        }

        public async Task CreateRequest(ExpenseRequest request)
        {
            await _expenseRequestRepository.AddAsync(request);
        }

        public List<ExpenseRequest> GetAll()
        {
            return _expenseRequestRepository.GetAllWithAppUserAsync().ToList();
        }

        public async Task<ExpenseRequest> GetRequestById(int id)
        {
            ExpenseRequest request= _expenseRequestRepository.GetById(id);
           return request;
        }
        public List<ExpenseRequest> PendingRequests(AppUser user)
        {
            var list = _expenseRequestRepository.GetPendingRequest(user);
            return list;
        }
        public List<ExpenseRequest> AllRequests(AppUser user)
        {
            var list = _expenseRequestRepository.GetAllRequests(user);
            return list;
        }

        public async Task UpdateAsync(ExpenseRequest request)
        {
            await _expenseRequestRepository.UpdateAsync(request);
        }

        public async Task DeleteAsync(ExpenseRequest request)
        {
            await _expenseRequestRepository.DeleteAsync(request);
        }
    }
}
