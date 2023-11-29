using AutoMapper;
using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;

        public CreateLeaveAllocationCommandHandler(IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IUserService userService)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _userService = userService;
        }
        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            //validate incoming Data
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("InValid Leave Allocation Request", validationResult);

            // Get Leave Type for the allocation
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            //Get Employees
            var employees = await _userService.GetEmployees();

            //Get Period
            var period = DateTime.Now.Year;

            //Assign Allocation if an allocation doesn't already exist for the Period and Leave type
            var allocations = new List<Domain.LeaveAllocation>();
            foreach (var emp in employees)
            {
                var allocationExists = await _leaveAllocationRepository.AllocationExists(emp.Id, leaveType.Id, period);

                if (allocationExists == false)
                {
                    allocations.Add(new Domain.LeaveAllocation
                    {
                        EmployeeId = emp.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period

                    });
                }
            }

            if (allocations.Any())
            {
                await _leaveAllocationRepository.AddAllocations(allocations);

            }

            //return record Id
            return Unit.Value;

        }

    }
}
