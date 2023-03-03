
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevJobsWeb;
using Entities;

namespace Contracts
{
    public interface IJobRepository: IRepositoryBase<Job>
    {
        IEnumerable<Job> GetAllJobs();
        Job GetJobById(int jobId);
        void CreateJob(Job job);
        void UpdateJob(Job job);
        void DeleteJob(Job job);  
        
    }
}
