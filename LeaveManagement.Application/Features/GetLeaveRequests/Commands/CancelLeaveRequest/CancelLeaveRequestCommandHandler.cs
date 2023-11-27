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
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<CancelLeaveRequestCommandHandler> _appLogger;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
            IEmailSender emailSender,IAppLogger<CancelLeaveRequestCommandHandler> appLogger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
            _appLogger = appLogger;
        }
        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if(leaveRequest is null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }


            leaveRequest.Cancelled = true;

            //If already approved, re-evaluate the employee's allocation for the leave Type


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
