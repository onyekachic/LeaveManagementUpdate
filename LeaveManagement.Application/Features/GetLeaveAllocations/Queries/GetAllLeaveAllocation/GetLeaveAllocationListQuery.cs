using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;

public record GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationListDto>>;

