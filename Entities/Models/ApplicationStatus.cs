using System;
using System.Collections.Generic;

namespace DevJobsWeb;

public partial class ApplicationStatus
{
    public int ApplicationSatusId { get; set; }

    public string? ApplicationStatusType { get; set; }

    public virtual ICollection<Application> Applications { get; } = new List<Application>();
}
