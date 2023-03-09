using Contracts;

using Entities.Models;
using Entities.ViewModels;
//using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class ApplicationRepository : RepositoryBase<Application>, IApplicationRepository
    {
        private readonly JobsOnLineContext _context;
        public ApplicationRepository(JobsOnLineContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        
        
        public  IEnumerable<Application> GetAllApplications()
        {
            return FindAll().OrderBy(application => application.JobId);
            
        }


        public IEnumerable<ApplicationViewModel> GetApplicationsWithDetails()
        {
            var appQuery = from b in _context.Applications
                           join c in _context.Applicants on b.ApplicantId equals c.ApplicantId
                           join d in _context.Jobs on b.JobId equals d.JobId
                           join e in _context.ApplicationStatuses on b.ApplicationStatusId equals e.ApplicationSatusId
                           select new ApplicationViewModel
                           {
                               Application = b,
                               Name = c.Name,
                               Lastname = c.LastName,
                               JobTitle = d.JobTitle,
                               JobPosition = d.PositionLevel,
                               ApplicationStatus = e.ApplicationStatusType,
                               
                           };
 


                                                
               return appQuery.ToList();
        }

        public Application GetApplicationById(int applicationId)
        {
            var test = from a in _context.Applicants
                       where a.ApplicantId == applicationId
                       select a;

            return FindByCondition(application => application.ApplicationId.Equals(applicationId)).FirstOrDefault();
        }

        public void CreateApplication(Application application)
        {
            Create(application);
        }

        public void UpdateApplication(Application application)
        {
            Update(application);
        }
        public void DeleteApplication(Application application)
        {
            Delete(application);
        }

    }
}
