using AutoMapper;
using LeaveManagement.Application.Features.GetLeaveRequests.Commands.CreateLeaveRequest;
using LeaveManagement.Application.Features.GetLeaveRequests.Commands.UpdateLeaveRequest;
using LeaveManagement.Application.Features.GetLeaveRequests.Queries.GetLeaveRequestDetails;
using LeaveManagement.Application.Features.GetLeaveRequests.Queries.GetLeaveRequests;
using LeaveManagement.Domain;

namespace LeaveManagement.Application.MappingProfiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequestListDto, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestDetailDto>();
            CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
        }
    }
}
