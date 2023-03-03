using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Repository;
using System.Net;

namespace DevJobsWeb.Controllers
{
    public class JobController : Controller
    {
       private readonly IRepositoryWrapper _repository;
       public JobController(IRepositoryWrapper repository )
       {                                                  
              _repository = repository;
       }

      
                          
        public IActionResult Index()
        {
            var jobs = _repository.Job.GetAllJobs();

            return View(jobs);
            
        }

        public IActionResult Details(int id)
        {
            var job = _repository.Job.GetJobById(id);  

            return View(job);
        }

      
        public IActionResult Edit(int id)
        {
            if(id == 0)   
            { 
                return RedirectToAction(nameof(Index)); 
            }  

            var _job = _repository.Job.GetJobById(id);
           
            if(_job == null)
            {

                return NotFound();
            }

            return View(new Job
            {
               JobId = _job.JobId,
               JobTitle = _job.JobTitle,
               PositionLevel = _job.PositionLevel,  
               Company = _job.Company,  
            } ) ;  
        }
        public IActionResult Edit(int id, Job job)
        {
            try
            {
                var _job = _repository.Job.GetJobById(id);
                if(job.JobTitle != null)
                {
                  job.JobTitle = _job.JobTitle;
                }

                if(job.PositionLevel != null)
                {
                    job.PositionLevel = _job.PositionLevel; 
                }
                if(job != null)
                { 
                    job.Company = _job.Company;
                }

                _repository.Job.UpdateJob(job);
                _repository.Save();

                return RedirectToAction(nameof(Index));    
                
            }
            catch 
            {
                return View();
            }

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var job = _repository.Job.GetJobById(id);

                if (job == null)
                    throw new Exception("Invalid ID");

                _repository.Job.Delete(job);
                _repository.Save();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View();
            }
        }
    }
}
