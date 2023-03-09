using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;


namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper 
    {
        private readonly JobsOnLineContext _repositoryContext;

        private IApplicantRepository _applicant;
        private IApplicationRepository _application;
        private IApplicationStatusRepository _applicationStatus;
        private IQualificationLevelRepository _qualificationLevel;
        private IJobRepository _job;


        

        public IApplicantRepository Applicant
        {
            get
            {
              if(_applicant == null)
                {
                    _applicant = new ApplicantRepository(_repositoryContext);
                
              }
                return _applicant;
            }
        }

        public IApplicationRepository Application
        {
            get
            {
                if (_application == null)
                {
                    _application = new ApplicationRepository (_repositoryContext);
                }
                return _application;
            }
        }

        public IApplicationStatusRepository ApplicationStatus
        {
             get {
                    if(_applicationStatus == null)
                    {
                       _applicationStatus = new ApplicationStatusRepository(_repositoryContext);    
                    }
                return _applicationStatus; 
             }
        }


        public IJobRepository Job
        {
            get
            {
                if (_job == null)
                {
                       _job = new JobRepository(_repositoryContext);
                }

                return _job;
            }

        }

        public IQualificationLevelRepository QualificationLevel
        {
            get
            {
                if (_qualificationLevel == null)
                {
                    _qualificationLevel = new QualificationLevelRepository(_repositoryContext);
                }
                return _qualificationLevel;
            }

        }

        public RepositoryWrapper(JobsOnLineContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }

    }

        



}
