using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;


namespace LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class EmployeeIndex
    {
        [Inject] ILeaveRequestService leaveRequestService { get; set; }

        [Inject] IJSRuntime js { get; set; }

        [Inject] NavigationManager NavigationManager { get; set; }

        public EmployeeLeaveRequestViewVM Model { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Model = await leaveRequestService.GetUserLeaveRequests();
        }

        async Task CancelRequestAsync(int id)
        {
            var confirm = await js.InvokeAsync<bool>("confirm", "Do you want to cancel this request?");

            if (confirm)
            {
                var response = await leaveRequestService.CancelLeaveRequest(id);

                if (response.Success)
                {
                    StateHasChanged();
                }
                else
                {
                    Message = response.Message;
                }

            }

        }

    }
}