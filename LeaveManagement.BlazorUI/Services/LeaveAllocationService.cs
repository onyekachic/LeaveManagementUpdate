using Blazored.LocalStorage;
using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Services.Base;

namespace LeaveManagement.BlazorUI.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        public LeaveAllocationService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {

        }

        public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                var response = new Response<Guid>();
                CreateLeaveAllocationCommand createLeaveAllocation = new() { LeaveTypeId = leaveTypeId };

                await _client.LeaveAllocationsPOSTAsync(createLeaveAllocation);

                return response;

            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
