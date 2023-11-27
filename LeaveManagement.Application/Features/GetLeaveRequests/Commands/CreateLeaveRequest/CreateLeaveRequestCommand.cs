using LeaveManagement.Application.Features.GetLeaveRequests.Shared;
using MediatR;

namespace LeaveManagement.Application.Features.GetLeaveRequests.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
    {
        public string RequestComments { get; set; } = string.Empty;
    }
}
