using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.BlazorUI.Pages
{
    public partial class Register
    {

        public RegisterVM Model { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string Message { get; set; }

        [Inject]

        private IAuthenticationService AuthenticationService { get; set; }

        protected override void OnInitialized()
        {
            Model = new RegisterVM();
        }

        public async Task HandleRegister()
        {
            var result = await AuthenticationService.RegisterAsync
                  (
                     Model.FirstName,
                     Model.LastName,
                     Model.UserName,
                     Model.Email,
                     Model.Password
                  );

             if(result == true)
            {
                NavigationManager.NavigateTo("/");
            }

            Message = "Something went wrong, please try again";
        }


    }
}