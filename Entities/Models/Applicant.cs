using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Applicant
{
    public int ApplicantId { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string Gender { get; set; } = null!;

    public string? Email { get; set; }

    public string? Address { get; set; }

    public int? QualificationLevelId { get; set; }

    public virtual ICollection<Application> Applications { get; } = new List<Application>();

    public virtual QualificationLevel? QualificationLevel { get; set; }
}
