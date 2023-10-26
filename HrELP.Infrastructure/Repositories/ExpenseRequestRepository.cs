using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.Repositories
{
    public class ExpenseRequestRepository : BaseRepository<ExpenseRequest>, IExpenseRequestRepository
    {
        public ExpenseRequestRepository(HrElpContext context) : base(context)
        {
        }

        public ExpenseRequest GetById(int id)
        {
            ExpenseRequest? expenseRequest = _table.Include(x => x.RequestType).ThenInclude(x=>x.RequestCategory).Include(x => x.AppUser).FirstOrDefault(x => x.Id == id);
            return expenseRequest;
        }

        IQueryable<ExpenseRequest> IExpenseRequestRepository.GetAllWithAppUserAsync()
        {
            IQueryable<ExpenseRequest> list = _table.Include(x => x.AppUser).Include(y => y.RequestType);
            
            return list;
        }
    }
}
