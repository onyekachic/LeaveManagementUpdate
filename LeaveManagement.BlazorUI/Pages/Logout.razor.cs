using Microsoft.AspNetCore.Components;
using LeaveManagement.BlazorUI.Contracts;

namespace LeaveManagement.BlazorUI.Pages
{
    public partial class Logout
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        private IAuthenticationService authService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await authService.Logout();
            NavigationManager.NavigateTo("/");
        }

    }
}