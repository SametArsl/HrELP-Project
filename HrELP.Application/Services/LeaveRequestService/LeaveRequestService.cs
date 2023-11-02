using AutoMapper;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Domain.Entities.Enums;
using HrELP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.LeaveRequestService
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task CreateLeaveRequestAsync(LeaveRequest request)
        {
            await _leaveRequestRepository.AddAsync(request);
        }

        public async Task DeniedLeaveRequestAsync(int id)
        {            
            await _leaveRequestRepository.DeleteAsync(await _leaveRequestRepository.GetFirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<LeaveRequest> GetLeaveRequestAsync(int leaveRequestId)
        {
           return  await _leaveRequestRepository.GetByIdAsync(leaveRequestId);
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestAsync()
        {
            return await _leaveRequestRepository.GetAllAsync(x=> x.IsActive==true);    
        }

        public async Task<List<LeaveRequest>> GetPermissionsByApprovalStatusAsync(ApprovalStatus approvalStatus, int id)
        {
            var permissions = await _leaveRequestRepository.GetAllAsync(x => x.IsActive == true);
            return permissions.Where(p => p.ApprovalStatus == approvalStatus && p.UserId == id).ToList();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestByStatusAsync(ApprovalStatus approvalStatus, int id, params Expression<Func<LeaveRequest, object>>[] includes)
        {
          var leaveRequest=await _leaveRequestRepository.GetAllAsync(x=>x.IsActive==true,p=>p.LeaveType); 
            return leaveRequest.Where(p=>p.ApprovalStatus==approvalStatus && p.UserId==id).ToList();            
        }

        public async  Task<List<LeaveRequest>> GetLeaveRequestByStatusAsync(int id, params Expression<Func<LeaveRequest, object>>[] includes)
        {
            var leaveRequest = await _leaveRequestRepository.GetAllAsync(x => x.IsActive == true, p => p.LeaveType);
            return leaveRequest;
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
        public async Task UpdateAsync(LeaveRequest request)
        {
            await _leaveRequestRepository.UpdateAsync(request);
        }

        public async Task DeleteAsync(LeaveRequest request)
        {
            await _leaveRequestRepository.DeleteAsync(request);
        }
    }
}
