﻿using AutoMapper;
using LeaveManagement.Application.Contracts.Logging;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _logger;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
                                  ILeaveTypeRepository leaveTypeRepository, 
                                  IMapper mapper,
                                  IAppLogger<UpdateLeaveAllocationCommandHandler> logger)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository,_leaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveAllocation), request.Id);
                throw new BadRequestException("InValid LeaveAllocation", validationResult);

            }

            var leaveAllocation =await _leaveAllocationRepository.GetByIdAsync(request.Id);

            if (leaveAllocation is null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            _mapper.Map(request,leaveAllocation);

            await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

            return Unit.Value;


        }
    }
}
