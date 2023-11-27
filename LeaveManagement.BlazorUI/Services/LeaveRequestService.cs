﻿using Blazored.LocalStorage;
using LeaveManagement.BlazorUI.Contracts;
using LeaveManagement.BlazorUI.Services.Base;

namespace LeaveManagement.BlazorUI.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        public LeaveRequestService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {

        }
    }
}
