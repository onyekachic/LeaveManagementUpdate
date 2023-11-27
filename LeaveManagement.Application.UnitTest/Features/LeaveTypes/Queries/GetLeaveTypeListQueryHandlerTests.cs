using AutoMapper;
using LeaveManagement.Application.Contracts.Logging;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using LeaveManagement.Application.MappingProfiles;
using LeaveManagement.Application.UnitTest.Mocks;
using Moq;
using Shouldly;

namespace LeaveManagement.Application.UnitTest.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeListQueryHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;
        private Mock<IAppLogger<GetLeaveTypeQueryHandler>> _mockAppLogger;

        public GetLeaveTypeListQueryHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockAppLogger = new Mock<IAppLogger<GetLeaveTypeQueryHandler>>();
                
        }

        [Fact]
        public async Task GetLeaveTypeListTests()
        {
            var handler = new GetLeaveTypeQueryHandler(_mapper, 
                _mockRepo.Object,
                _mockAppLogger.Object);

            var result = await handler.Handle(new GetLeaveTypeQuery(),
                CancellationToken.None);

            result.ShouldBeOfType<List<LeaveTypeDto>>();
            result.Count.ShouldBe(3);
        }
    }
}
