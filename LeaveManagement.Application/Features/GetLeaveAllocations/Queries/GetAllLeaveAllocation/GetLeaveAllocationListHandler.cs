using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;

public class GetLeaveAllocationListHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationListDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationListHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }
    public  async Task<List<LeaveAllocationListDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        //To Add Later
        //- Get records for specific user
        // - Get allocations per employee

        var leaveallocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();

        var allocations = _mapper.Map<List<LeaveAllocationListDto>>(leaveallocations);

        return allocations;


    }
}
