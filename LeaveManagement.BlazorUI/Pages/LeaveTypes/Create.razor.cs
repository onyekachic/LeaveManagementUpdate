using Microsoft.AspNetCore.Components;
using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Authorization;
using Blazored.Toast.Services;

namespace LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Create
    {
        [Inject] NavigationManager _navManager { get; set; }

        [Inject] IToastService _toastService { get; set; }  

        [Inject] ILeaveTypeService _client { get; set; }

        public string Message { get; private set; }

        LeaveTypeVM leaveType = new LeaveTypeVM();
        async Task CreateLeaveType()
        {
            var response = await _client.CreateLeaveType(leaveType);
            if (response.Success)
            {
                _toastService.ShowSuccess("Leave Type Created Successfully");
                _toastService.ShowToast(ToastLevel.Info, "Test");
                _navManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
    }
}