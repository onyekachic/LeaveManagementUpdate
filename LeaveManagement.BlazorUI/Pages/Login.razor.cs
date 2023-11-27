using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.BlazorUI.Pages
{
    public partial class Login
    {

        public LoginVM Model { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string Message { get; set; }

        [Inject]

        private IAuthenticationService AuthenticationService { get; set; }

        public Login()
        {

        }

        protected override void OnInitialized()
        {
            Model = new LoginVM();
        }

        public async Task HandleLogin()
        {
            if (await AuthenticationService.AuthenticationAsync(Model.Email,Model.Password))
            {
                NavigationManager.NavigateTo("/");
            }

            Message = "UserName/Password combination unknown";
        }


    }
}