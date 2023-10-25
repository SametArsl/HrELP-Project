using AutoMapper;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
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
    }
}
