using AutoMapper;
using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Contracts.Logging;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.GetLeaveRequests.Commands.UpdateLeaveRequest;
using LeaveManagement.Application.Models.Email;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.GetLeaveRequests.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;
        private readonly IUserService _userService;

        public CreateLeaveRequestCommandHandler(ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IEmailSender emailSender,
            IMapper mapper,
            IAppLogger<CreateLeaveRequestCommandHandler> appLogger,
            IUserService userService)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
            _emailSender = emailSender;
            _mapper = mapper;
            _appLogger = appLogger;
            _userService = userService;
        }
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("InValid LeaveR equest", validationResult);

            }

            // Get requesting employee's id
            
            var employeeId = _userService.UserId;

            //check on employee's allocation

            var allocation  = await _leaveAllocationRepository.GetUserAllocations(employeeId, request.LeaveTypeId);

            // if allocation aren't enough, return validation error with message

            if(allocation is null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveTypeId),
                    "You do not have any allocations for this leave type"));
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }

            //Check if there is a remaining allocation for the employee
            int daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;

            if(daysRequested > allocation.NumberOfDays) 
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.EndDate),
                    "You do not have any allocations for this leave type"));
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }



            //create leave request

            var leaveRequestToCreate = _mapper.Map<Domain.LeaveRequest>(request);
            leaveRequestToCreate.RequestingEmployeeId=employeeId;
            leaveRequestToCreate.DateRequested = DateTime.Now;
            await _leaveRequestRepository.CreateAsync(leaveRequestToCreate);

            try
            {
                //send confirmation email
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get email from employee record */
                    Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D}" +
                          $"has been submitted successfully.",
                    Subject = "Leave Request Submitted"
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
