using Contracts;
using Microsoft.AspNetCore.Http;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

using Entities.Enums;
using System.ComponentModel;


namespace DevJobsWeb.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IRepositoryWrapper _repository;    
        public ApplicationController(IRepositoryWrapper repositoryContext)
        {
            _repository = repositoryContext;

        }
        // GET: ApplicationController
        [Route("/test")]
        public ActionResult Index()                                                      
        {
            var app = _repository.Application.GetApplicationsWithDetails();
            return View(app);
        }

        // GET: ApplicationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicationController/Create
        public ActionResult Create(int id)
        {
             

           // var app = _repository.Application.GetApplicationById(newID);
           // if(app == null)
            
                return View( new Application()
                {
                  JobId = id,
                //  ApplicationId = newID,
                  ApplicationStatusId = (int)DatabaseEnums.ApplicationStatus.Submitted,
                  DateCreated = DateTime.Now,   

                }
                  );

            return View();
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Application application)
        {
            var appID = _repository.Applicant.GetApplicantById(application.ApplicantId);

            
            var num2 = application.ApplicantId;
             
            try
            {
                Random random = new();
               
                int newID = random.Next(4, 51);

                var app = _repository.Application.GetApplicationById(newID);

                
                    if (app == null)
                    {
                        _repository.Application.CreateApplication(new Application()
                        {
                            ApplicationId = newID,
                            ApplicantId = application.ApplicantId,
                            JobId = application.JobId,                  // picking from available jobs
                            DateCreated = application.DateCreated,
                            ApplicationStatusId = application.ApplicationStatusId,

                        });

                        _repository.Save();

                        return RedirectToAction(nameof(Index));
                    }

                 return View();

            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: ApplicationController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return View(nameof(Index));
            }
            
            var application = _repository.Application.GetApplicationById(id);   
            return View( new Application()
            {
                
                ApplicantId = application.ApplicantId,
                JobId = application.JobId,
                ApplicationStatusId = application.ApplicationStatusId,
               
            }
            );
        }

        // POST: ApplicationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Application application)
        {
            try
            {
                var app = _repository.Application.GetApplicationById(id);
                if (app == null)
                {
                    return NotFound();
                }
                //Status of the applicatin should be the only thing edited about the application

                app.ApplicationStatusId = application.ApplicationStatusId;
                _repository.Application.UpdateApplication(app);

                _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationController/Delete/5
        public ActionResult Delete(int id)
        {

            var app = _repository.Application.GetApplicationById(id);
            if(app == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(app);
        }

        // POST: ApplicationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Application application)
        {
            try
            {
                _repository.Application.DeleteApplication(application);
                _repository.Save();
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
