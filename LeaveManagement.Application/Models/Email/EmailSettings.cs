﻿namespace LeaveManagement.Application.Models.Email
{
    public class EmailSettings
    {
        public string Apikey { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;  
    }
}
