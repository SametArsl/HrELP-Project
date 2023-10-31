using AutoMapper;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
using HrELP.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.AdvanceRequestService
{
    public class AdvanceRequestService:IAdvanceRequestService
    {
        private readonly IAdvanceRequestRepository _advanceRequestRepository;

        public AdvanceRequestService(IAdvanceRequestRepository advanceRequestRepository)
        {
            _advanceRequestRepository = advanceRequestRepository;
        }

        public async Task CreateRequest(AdvanceRequest request)
        {
            await _advanceRequestRepository.AddAsync(request);
        }

        public List<AdvanceRequest> GetAll()
        {
            return _advanceRequestRepository.GetAllWithAppUserAsync().ToList();
        }

        public async Task<AdvanceRequest> GetRequestById(int id)
        {
            AdvanceRequest request = _advanceRequestRepository.GetById(id);
            return request;
        }

        public async Task UpdateAsync(AdvanceRequest request)
        {
            await _advanceRequestRepository.UpdateAsync(request);
        }
    }
}
