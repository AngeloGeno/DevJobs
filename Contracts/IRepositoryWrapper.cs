using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;


namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IApplicantRepository Applicant { get; }
        IApplicationRepository Application { get; }
        IApplicationStatusRepository ApplicationStatus { get; } 
        IJobRepository Job { get; }
        IQualificationLevelRepository QualificationLevel { get; }
        void Save();
        Task SaveAsync();
        
    }
}
