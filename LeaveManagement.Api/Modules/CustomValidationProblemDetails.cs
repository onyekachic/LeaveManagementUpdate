﻿using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Api.Modules
{
    public class CustomProblemDetails : ProblemDetails
    {
      public  IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
