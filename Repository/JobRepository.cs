using Contracts;
using DevJobsWeb;
//using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        private readonly JobsOnLineContext _context;
        public JobRepository(JobsOnLineContext repositoryContext) : base(repositoryContext)
        {
                _context = repositoryContext;   
        }

        public IEnumerable<Job> GetAllJobs()
        {

            return FindAll().OrderBy(job => job.JobTitle).ToList();
        }
        public Job GetJobById(int jobId)
        {
            return FindByCondition(job => job.JobId.Equals(jobId)).FirstOrDefault();
        }

        public void CreateJob(Job job)
        {
            Create(job);
        }

        public void UpdateJob(Job job)
        {
            Update(job);
        }

        public void DeleteJob(Job job)
        { 
            Delete(job);
        }

    }
}
