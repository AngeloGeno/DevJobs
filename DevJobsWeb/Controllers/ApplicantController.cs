using Contracts;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using DevJobsAPI;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace DevJobsWeb.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IRepositoryWrapper _repository;    

        public ApplicantController( IRepositoryWrapper repository)
        {
           // _logger = logger;
            _repository = repository;   
        }


        [Authorize]
        public ActionResult Index()                                             
        {
            var app = _repository.Applicant.ApplicantsWithQualificationName();
            return View(app);
        }

        [Route("applicant/applicantDetails/{Id}")]
        [Authorize]
        public ActionResult Details(int id)
        {

            if (id == 0 || id == null)
            {

                return RedirectToAction(nameof(Index));
            }
            var applicantDetails = _repository.Applicant.GetApplicantById(id);

                
                return View(applicantDetails);  
        }

     
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: ApplicantsController/Create
        [HttpPost]
       
        public IActionResult Create(Applicant applicant)
        {
            try
            {

                _repository.Applicant.CreateApplicant(new Applicant()
                {
                    ApplicantId = applicant.ApplicantId,
                    Name = applicant.Name,
                    LastName = applicant.LastName,
                    Address = applicant.Address,
                    Gender = applicant.Gender,
                    QualificationLevelId = applicant.QualificationLevelId,

                } );
                _repository.Save();

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicantsController/Edit/5
        //[Authorize]
        public ActionResult Edit(int id)
        {
            if(id == 0|| id == null)
            {

                return RedirectToAction (nameof(Index));  
            }
            var app = _repository.Applicant.GetApplicantById(id);

            if (app == null)
            {
                return NotFound();
            }
            return View( new Applicant()
            {
               Name = app.Name,
               LastName = app.LastName,
               Gender = app.Gender,
               Address = app.Address,
               QualificationLevelId = app.QualificationLevelId, 
                                                                                 
            });
        }

        // POST: ApplicantsController/Edit/5
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> Edit(int id, Applicant applicant)                                                                        
        {
            try
            {
                var appRecord = _repository.Applicant.GetApplicantById(id); 

                 if(appRecord == null)
                 {
                    return NotFound();
                 }

                appRecord.Name = applicant.Name;
                appRecord.LastName = applicant.LastName;
                appRecord.Gender = applicant.Gender;
                appRecord.Address = applicant.Address;
                appRecord.QualificationLevelId = applicant.QualificationLevelId;
                _repository.Applicant.UpdateApplicant(appRecord);
                await _repository.SaveAsync();
                
                var a = _repository.Applicant.GetApplicantById(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicantsController/Delete/5
        [HttpGet]
    
        public ActionResult Delete(int id)
        {
            try 
            {
                var applicant = _repository.Applicant.GetApplicantById(id);
                return View(applicant);
            }
            catch 
            {

                return RedirectToAction(nameof(Index));
            }
          
        }

        // POST: ApplicantsController/Delete/5
 
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                var app = _repository.Applicant.GetApplicantById(id);

                if (app == null)
                {
                    return NotFound();
                }
              
                _repository.Applicant.DeleteApplicant(app);
                _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
