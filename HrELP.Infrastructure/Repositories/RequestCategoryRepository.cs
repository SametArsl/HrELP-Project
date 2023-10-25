using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.Repositories
{
    public class RequestCategoryRepository : BaseRepository<RequestCategory>, IRequestCategoryRepository
    {
        public RequestCategoryRepository(HrElpContext context) : base(context)
        {
        }
    }
}
