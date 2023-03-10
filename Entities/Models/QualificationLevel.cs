using System;
using System.Collections.Generic;

namespace DevJobsWeb;

public partial class QualificationLevel
{
    public int QualificationLevelId { get; set; }

    public string? QualificationLevelName { get; set; }

    public virtual ICollection<Applicant> Applicants { get; } = new List<Applicant>();
}
