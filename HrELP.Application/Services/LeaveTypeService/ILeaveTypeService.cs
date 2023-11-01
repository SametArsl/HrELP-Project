using HrELP.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.LeaveTypeService
{
    public interface ILeaveTypeService
    {
        Task<LeaveType> GetLeaveTypeAsync(int leaveTypeId);
        Task<LeaveType> GetLeaveTypeByNameAsync(string name);
        Task CreateLeaveTypeAsync(LeaveType leaveType);
        Task<List<LeaveType>> GetAllLeaveTypesAsync();
    }
}
