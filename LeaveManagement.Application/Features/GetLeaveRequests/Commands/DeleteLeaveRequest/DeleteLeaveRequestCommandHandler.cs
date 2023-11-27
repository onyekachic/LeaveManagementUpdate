using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using MediatR;

namespace LeaveManagement.Application.Features.GetLeaveRequests.Commands.DeleteLeaveRequest
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand,Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var LeaveRequestToDelete = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (LeaveRequestToDelete is null)
            {
              throw new NotFoundException(nameof(LeaveRequestToDelete), request.Id);
            }

            await _leaveRequestRepository.DeleteAsync(LeaveRequestToDelete);

            return Unit.Value;
        }
    }
}
