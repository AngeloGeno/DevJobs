
using Contracts;
using DevJobsWeb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevJobsAPI.Controllers
{
    [Route("api/job/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
      
        public JobController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
         
        }

        [HttpGet]

        public IActionResult GetAllJobs()
        {
            try
            {
                var job = _repository.Job.GetAllJobs();
                _logger.LogInfo($"Returned all Jobs from DB.");

                return Ok(job);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllJobds action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetJobById(int Id)
        {
            try
            {
                var job = _repository.Job.GetJobById(Id);

                if (job is null)
                {
                    _logger.LogError($"Job with id: {Id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned job with id: {Id}");
                    
                    return Ok(job);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetJobById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }


        [HttpPost]
        public IActionResult CreateJob([FromBody] Job job)
        {
            try
            {
                if (job is null)
                {
                    _logger.LogError("Job object sent from client is null.");
                    return BadRequest("Job object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid job object sent from client.");
                    return BadRequest("Invalid model object");
                }
                _repository.Job.CreateJob(new Job()
                {
                    JobId = job.JobId,
                    JobTitle = job.JobTitle,
                    PositionLevel = job.PositionLevel,
                    Company = job.Company

                });
                
                _repository.Save();
                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateJob action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }                                                                                                          
        }

        [HttpPut("{id}")]
        public IActionResult UpdateJob(int id, [FromBody] Job job )
        {
            try
            {
                if (job is null)
                {
                    _logger.LogError("Job object sent from client is null.");
                    return BadRequest("Job object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Job object sent from client.");
                    return BadRequest("Invalid model object");
                }


                var jobEntity = _repository.Job.GetJobById(id);
                if (jobEntity is null)
                {
                    _logger.LogError($"Job with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                jobEntity.JobTitle = job.JobTitle;
                jobEntity.PositionLevel = job.PositionLevel;
                jobEntity.Company = job.Company;

                _repository.Job.UpdateJob(jobEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateJob action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJob(int id)
        {
            try
            {
                var job = _repository.Job.GetJobById(id);
                if (job == null)
                {
                    _logger.LogError($"Job with id: {id}, hasn't been found in db.");
                    return NotFound();
                }


                _repository.Job.DeleteJob(job);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteJob action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
