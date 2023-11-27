using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.GetLeaveRequests.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestDetailQuery : IRequest<LeaveRequestDetailDto>
    {
        public int Id { get; set; }
    }
}
