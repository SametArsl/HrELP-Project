using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Repositories;
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
        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository)
        {
            _ILeaveTypeRepository = leaveTypeRepository;
        }
        public async Task CreateLeaveTypeAsync(LeaveType leaveType)
        {
           await _ILeaveTypeRepository.AddAsync(leaveType);
        }

        public async Task<List<LeaveType>> GetAllLeaveTypesAsync()
        {
            return await _ILeaveTypeRepository.GetAllAsync(x => x.IsActive == true);
        }

        public async Task<LeaveType> GetLeaveTypeAsync(int leaveTypeId)
        {
            LeaveType leaveType = await _ILeaveTypeRepository.GetByIdAsync(leaveTypeId);
            return leaveType;
        }

        public async Task<LeaveType> GetLeaveTypeByNameAsync(string name)
        {
            return await _ILeaveTypeRepository.GetFirstOrDefaultAsync(p=>p.LeaveTypeName == name);
        }
    }
}
