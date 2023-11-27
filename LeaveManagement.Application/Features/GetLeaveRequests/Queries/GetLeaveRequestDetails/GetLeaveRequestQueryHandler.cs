using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.GetLeaveRequests.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<LeaveRequestDetailDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        {

            var leaveRequest = _mapper.Map<LeaveRequestDetailDto>(
                await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));


            // Add Employee details as needed;

            return leaveRequest;
        }
    }
}
