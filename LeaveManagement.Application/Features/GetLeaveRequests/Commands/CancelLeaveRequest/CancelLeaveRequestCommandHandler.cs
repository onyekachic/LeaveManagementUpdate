using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Logging;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Models.Email;
using MediatR;

namespace LeaveManagement.Application.Features.GetLeaveRequests.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<CancelLeaveRequestCommandHandler> _appLogger;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IEmailSender emailSender,
            IAppLogger<CancelLeaveRequestCommandHandler> appLogger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
            _emailSender = emailSender;
            _appLogger = appLogger;
        }
        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }


            leaveRequest.Cancelled = true;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            //If already approved, re-evaluate the employee's allocation for the leave Type
            if (leaveRequest.Approved == true)
            {
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                var allocation = await _leaveAllocationRepository.GetUserAllocations
                    (leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);

                allocation.NumberOfDays += daysRequested;

                await _leaveAllocationRepository.UpdateAsync(allocation);
               
            }

            try
            {
                //send confirmation email
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get email from employee record */
                    Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D}" +
                          $"has been cancelled successfully.",
                    Subject = "Leave Request cancelled"
                };

                await _emailSender.SendEmail(email);

            }
            catch (Exception ex)
            {

                _appLogger.LogWarning(ex.Message);
            }

            return Unit.Value;
        }
    }
}
