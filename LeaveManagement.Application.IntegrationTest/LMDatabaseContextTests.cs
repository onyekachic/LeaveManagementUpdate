using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace LeaveManagement.Application.IntegrationTest
{
    public class LMDatabaseContextTests
    {
        private readonly LMDatabaseContext _lmDatabaseContext;

        public LMDatabaseContextTests()
        {
           var dbOptions = new DbContextOptionsBuilder<LMDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _lmDatabaseContext = new LMDatabaseContext(dbOptions);
        }
        [Fact]
        public async void Save_SetDateCreatedValue()
        {
            //Arrange
            var leaveType = new LeaveType
           {
             
                   Id = 1,
                   DefaultDays = 10,
                   Name = "Test Vacation"
               
            };

            // Act
            await _lmDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await _lmDatabaseContext.SaveChangesAsync();

            // Assert

            leaveType.DateCreated.ShouldNotBeNull();


        }
            

        [Fact]
        public async void Save_SetDateModifiedValue()
        {

            var leaveType = new LeaveType
            {

                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"

            };

            // Act
            await _lmDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await _lmDatabaseContext.SaveChangesAsync();

            // Assert

            leaveType.DateModified.ShouldNotBeNull();

        }
    }
}