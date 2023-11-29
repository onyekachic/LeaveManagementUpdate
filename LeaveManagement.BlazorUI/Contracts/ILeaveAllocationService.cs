using LeaveManagement.BlazorUI.Models.LeaveTypes;
using LeaveManagement.BlazorUI.Services.Base;

namespace LeaveManagement.BlazorUI.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId);
    }
}
