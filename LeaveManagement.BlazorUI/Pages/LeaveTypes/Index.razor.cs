using Blazored.Toast.Services;
using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILeaveTypeService LeaveTypeService { get; set; }

        [Inject] IToastService _toastService { get; set; }

        [Inject]
        public ILeaveAllocationService LeaveAllocationService { get; set; }

        public List<LeaveTypeVM> LeaveTypes { get; private set; }

        public string Message { get; set; } = string.Empty;

        protected void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/leavetypes/create/");
        }

        protected void AllocateLeaveType(int id)
        {
            // Use Leave Allocation Service here
            LeaveAllocationService.CreateLeaveAllocations(id);
        }

        protected void EditLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/edit/{id}");

        }

        protected void DetailsLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/details/{id}");

        }

        protected async Task DeleteLeaveType(int id)
        {
            var response = await LeaveTypeService.DeletLeaveType(id);
            if(response.Success)
            {
                _toastService.ShowSuccess("Leave Type Successfully Deleted");
                await OnInitializedAsync();

            }
            else
            {
                Message = response.Message;
            }

        }

        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await LeaveTypeService.GetLeaveTypes();  
        }
    }
}