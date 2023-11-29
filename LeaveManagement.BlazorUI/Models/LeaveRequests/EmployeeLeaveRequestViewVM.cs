using LeaveManagement.BlazorUI.Models.LeaveAllocations;
using LeaveManagement.BlazorUI.Models.LeaveRequests;

namespace LeaveManagement.BlazorUI.Models.LeaveRequests
{
    public class EmployeeLeaveRequestViewVM
    {
        public List<LeaveAllocationVM> LeaveAllocations { get; set; } = new List<LeaveAllocationVM>();
        public List<LeaveRequestVM> LeaveRequests { get; set; } = new List<LeaveRequestVM>();
    }
}