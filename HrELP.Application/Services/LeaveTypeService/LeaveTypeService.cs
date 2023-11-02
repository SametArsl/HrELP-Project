using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Repositories;
using HrELP.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.LeaveTypeService
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _ILeaveTypeRepository;
        private readonly ILeaveRequestRepository _ILeaveRequestRepository;
        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository ILeaveRequestRepository)
        {
            _ILeaveTypeRepository = leaveTypeRepository;
            _ILeaveRequestRepository = ILeaveRequestRepository;
        }
        public async Task CreateLeaveTypeAsync(LeaveTypes leaveType)
        {
           await _ILeaveTypeRepository.AddAsync(leaveType);
        }

        public async Task<List<LeaveTypes>> GetAllLeaveTypesAsync()
        {
            return await _ILeaveTypeRepository.GetAllAsync(x => x.IsActive == true);
        }

        public async Task<LeaveTypes> GetLeaveTypeAsync(int leaveTypeId)
        {
            LeaveTypes leaveType = await _ILeaveTypeRepository.GetByIdAsync(leaveTypeId);
            return leaveType;
        }

        public async Task<LeaveTypes> GetLeaveTypeByNameAsync(string name)
        {
            return await _ILeaveTypeRepository.GetFirstOrDefaultAsync(p=>p.LeaveTypeName == name);
        }

        public async Task<LeaveTypes> GetLeaveTypesAsync(int leaveRequesId)
        {
            var leaveRequest = _ILeaveRequestRepository.GetById(leaveRequesId);
            var leavetype = await _ILeaveTypeRepository.GetByIdAsync(leaveRequest.LeaveTypeId);

            return leavetype;
        }
    }
}
