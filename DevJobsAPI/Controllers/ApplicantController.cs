
using Contracts;
using Microsoft.AspNetCore.Mvc;
using DevJobsWeb;
using System.Linq.Expressions;
using static Entities.Enums.DatabaseEnums;
using System.Net;
using System.Reflection;


namespace DevJobsAPI.Controllers
{
    [Route("api/applicant/[action]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;

        public ApplicantController(ILoggerManager logger, IRepositoryWrapper repository)
        { 
            _logger = logger;
            _repository = repository;
         
        }

        [HttpGet]
        public IActionResult GetAllApplicants()
        {
            try
            {
                var applicants = _repository.Applicant.ApplicantsWithQualificationName();
                _logger.LogInfo($"Return all applicants from DB.");
                return Ok(applicants);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllApplicants action: {ex.Message}");
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server Error ");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetApplicantById(int Id)
        {
            try 
            {
                var applicant = _repository.Applicant.GetApplicantById(Id);

                if (applicant is null)
                {
                    _logger.LogError($"Applicant with id: {Id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned applicant with id: {Id}");
                   
                    return Ok(applicant);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetApplicantById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }


        [HttpPost("{id}")]

        public IActionResult CreateApplicant([FromBody] Applicant applicant)
        {
            try
            {
                if (applicant is null)
                {
                    _logger.LogError("Applicant object sent from client is null.");
                    return BadRequest("Applicant object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Applicant object sent from client.");
                    return BadRequest("Invalid model object");
                }



                _repository.Applicant.CreateApplicant(new Applicant
                {
                    ApplicantId = applicant.ApplicantId,
                    Name = applicant.Name,
                    LastName = applicant.LastName,
                    Address = applicant.Address,
                    Gender = applicant.Gender,
                    QualificationLevelId = applicant.QualificationLevelId,
                });
                    _repository.Save();

                return Ok("Success") ;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateApplicant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateApplicant(int id, [FromBody] Applicant applicant)
        {
            try
            {
                if (applicant is null)
                {
                    _logger.LogError("Applicant object sent from client is null.");
                    return BadRequest("Applicant object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Applicant object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var app = _repository.Applicant.GetApplicantById(id);
                if (app is null)
                {
                    _logger.LogError($"Applicant with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                app.Name = applicant.Name;
                app.LastName = applicant.LastName;
                app.Gender = applicant.Gender;
                app.QualificationLevelId = applicant.QualificationLevelId;
                app.Address = applicant.Address;
                 

                _repository.Applicant.UpdateApplicant(app);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateApplicant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public IActionResult DeleteApplicant(int id)
        {
            try
            {
                var applicant = _repository.Applicant.GetApplicantById(id);
                if (applicant == null)
                {
                    _logger.LogError($"Applicant with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Applicant.DeleteApplicant(applicant);
                _repository.Save();

                return NoContent();    
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteApplicant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
