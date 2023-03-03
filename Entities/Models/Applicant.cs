using System;
using System.Collections.Generic;

namespace DevJobsWeb;

public partial class Applicant
{
    public int ApplicantId { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string Gender { get; set; } = null!;

    public string? Address { get; set; }

    public int? QualificationLevelId { get; set; }

    public virtual Application? Application { get; set; }
                                                   
    public virtual QualificationLevel? QualificationLevel { get; set; }
}
