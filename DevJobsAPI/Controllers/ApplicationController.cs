
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevJobsAPI.Controllers
{
    [Route("api/application/[action]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {

        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
       // private IMapper _mapper;
        public ApplicationController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
          
        }

        [HttpGet]
        public IActionResult GetAllApplications()
        {
            try
            {
                var application = _repository.Application.GetAllApplications();
                _logger.LogInfo($"Returned all applications from DB.");
                
                return Ok(application);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllApplications action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }


        }

        [HttpGet("{id}")]
        public IActionResult GetApplicationById(int Id)
        {
            try
            {
                var application = _repository.Application.GetApplicationById(Id);

                if (application is null)
                {
                    _logger.LogError($"Application with id: {Id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned application with id: {Id}");
                 
                    return Ok(application);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetApplicationById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }


        [HttpPost]

        public IActionResult CreateApplication([FromBody] Application application)
        {
            try
            {
                if (application is null)
                {
                    _logger.LogError("Application object sent from client is null.");
                    return BadRequest("Application object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Application object sent from client.");
                    return BadRequest("Invalid model object");
                }

                Random random = new Random();

                int newID = random.Next(4, 51);

                var app = _repository.Application.GetApplicationById(newID);
                 _repository.Application.CreateApplication(new Application()
                {
                  
                   
                        ApplicationId = newID,
                        ApplicantId = application.ApplicantId,
                        JobId = application.JobId,                  
                        DateCreated = application.DateCreated,
                        ApplicationStatusId = application.ApplicationStatusId,

                 });
                _repository.Save();

                return CreatedAtRoute("ApplicationById", new { id = application.ApplicationId }, application);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateApplication action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateApplication(int id, [FromBody] Application application)
        {
            try
            {
                if (application is null)
                {
                    _logger.LogError("Application object sent from client is null.");
                    return BadRequest("Application object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Application object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var appRecord = _repository.Application.GetApplicationById(id);



                if (appRecord is null)
                {
                    _logger.LogError($"Application with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
               
                appRecord.ApplicationStatusId = application.ApplicationStatusId;

                _repository.Application.UpdateApplication(appRecord);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateApplication action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteApplication(int id)
        {
            try
            {
                var application = _repository.Application.GetApplicationById(id);
                if (application == null)
                {
                    _logger.LogError($"Application with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Application.DeleteApplication(application);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteApplication action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}

