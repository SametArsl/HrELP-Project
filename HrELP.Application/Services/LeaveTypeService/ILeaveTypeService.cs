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
        Task<LeaveTypes> GetLeaveTypeAsync(int leaveTypeId);
        Task<LeaveTypes> GetLeaveTypeByNameAsync(string name);
        Task CreateLeaveTypeAsync(LeaveTypes leaveType);
        Task<List<LeaveTypes>> GetAllLeaveTypesAsync();
        Task<LeaveTypes> GetLeaveTypesAsync(int leaveRequestId);
    }
}
