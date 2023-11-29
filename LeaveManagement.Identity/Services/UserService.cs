﻿using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Models.Identity;
using LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserManager<ApplicationUser> userManager,
              IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager; 
            _contextAccessor = contextAccessor;
        }

        public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid"); }


        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                Firstname = employee.FirstName,
                Lastname = employee.LastName
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(q => new Employee 
            { 
                Id = q.Id, 
                Firstname = q.FirstName,
                Lastname = q.LastName
            }).ToList();
        }
    }
}
