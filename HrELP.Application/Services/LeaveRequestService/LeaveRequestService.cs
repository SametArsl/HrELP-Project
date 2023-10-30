using AutoMapper;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Repositories;
using HrELP.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.LeaveRequestService
{
    public class LeaveRequestService:ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task CreateRequest(LeaveRequest request)
        {
            await _leaveRequestRepository.AddAsync(request);
        }

        public List<LeaveRequest> GetAll()
        {
            return _leaveRequestRepository.GetAllWithAppUserAsync().ToList();
        }

        public async Task<LeaveRequest> GetRequestById(int id)
        {
            LeaveRequest request = _leaveRequestRepository.GetById(id);
            return request;
        }
    }
}
