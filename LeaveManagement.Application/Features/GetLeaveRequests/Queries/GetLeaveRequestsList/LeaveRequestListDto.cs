using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using LeaveManagement.Application.Models.Identity;

namespace LeaveManagement.Application.Features.GetLeaveRequests.Queries.GetLeaveRequests
{
    public  class LeaveRequestListDto
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public string RequestingEmployeeId { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Approved { get; set; }
        public bool? Cancelled { get; set; }
    }
}
