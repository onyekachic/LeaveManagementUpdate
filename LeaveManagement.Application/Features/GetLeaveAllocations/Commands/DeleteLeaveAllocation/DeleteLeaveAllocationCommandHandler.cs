﻿using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand,Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocationToDelete = await _leaveAllocationRepository.GetByIdAsync(request.Id);

            //verify that record exist
            if (leaveAllocationToDelete is null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            await _leaveAllocationRepository.DeleteAsync(leaveAllocationToDelete);

            return Unit.Value;

        }

        
    }
}
