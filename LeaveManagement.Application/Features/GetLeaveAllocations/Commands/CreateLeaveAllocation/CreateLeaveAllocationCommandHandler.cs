using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand,Unit>
    {
       
            private readonly IMapper _mapper;
            private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository)
            {
                _mapper = mapper;
                _leaveAllocationRepository = leaveAllocationRepository;
                _leaveTypeRepository = leaveTypeRepository;
        }
            public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
            {
                //validate incoming Data
                var validator = new CreateLeaveAllocationCommandValidator(_leaveAllocationRepository);
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (validationResult.Errors.Any())
                    throw new BadRequestException("InValid Leave Allocation Request", validationResult);

               // Get Leave Type for the allocation

               var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

               //Get Employees

               //Get Period

               //Assign Allocation

                //Convert to damain object
                var leaveAllocationTocreate = _mapper.Map<Domain.LeaveAllocation>(request);

                //add to database
                await _leaveAllocationRepository.CreateAsync(leaveAllocationTocreate);

                //return record Id
                return  Unit.Value;

            }

    }
}
