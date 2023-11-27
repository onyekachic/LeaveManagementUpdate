using MediatR;

namespace LeaveManagement.Application.Features.GetLeaveRequests.Queries.GetLeaveRequests
{
    public  class GetLeaveRequestListQuery : IRequest<List<LeaveRequestListDto>>
    {
    }
}
