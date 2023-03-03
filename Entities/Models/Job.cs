using System;
using System.Collections.Generic;

namespace DevJobsWeb;

public partial class Job
{
    public int JobId { get; set; }

    public string? JobTitle { get; set; }

    public string? PositionLevel { get; set; }

    public string? Company { get; set; }

    public virtual ICollection<Application> Applications { get; } = new List<Application>();
}
