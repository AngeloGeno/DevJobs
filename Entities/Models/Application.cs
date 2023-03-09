using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public int JobId { get; set; }

    public int ApplicantId { get; set; }

    public DateTime? DateCreated { get; set; }

    public int ApplicationStatusId { get; set; }

    public virtual Applicant Applicant { get; set; } = null!;

    public virtual ApplicationStatus ApplicationStatus { get; set; } = null!;

    public virtual Job Job { get; set; } = null!;
}
