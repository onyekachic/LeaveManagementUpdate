﻿using LeaveManagement.Application.Models.Identity;

namespace LeaveManagement.Application.Contracts.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(string userId);
    public string UserId { get;  }

}
