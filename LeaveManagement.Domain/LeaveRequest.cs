﻿using LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Domain;

public class LeaveRequest : BaseEntity
{
    public DateTime EndDate { get; set; }
    public DateTime StartDate { get; set; }
    [ForeignKey("LeaveTypeId")]
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComment { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public string RequestingEmployeeId { get; set; } = string.Empty;
}
