﻿using Contracts;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevJobsWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(IRepositoryWrapper repository, UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager)
        {
            _repository = repository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    
                };

                 var result = await userManager.CreateAsync(user,model.Password);

                if(result.Succeeded)
                {                                                              
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");

                }

                foreach (var error in result.Errors)
                {

                    ModelState.AddModelError(string.Empty,error.Description); 
                }

            }


            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string?returnUrl)
        {
            if (ModelState.IsValid)
            {
                  var result = await signInManager.PasswordSignInAsync( model.Email, model.Password, model.RememberMe, false);                        

                if(result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                
                    
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                
            }
            return View(model);

           
        }

        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
             var user = await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email{email}is already in use.");
            }

          


            
        }
    }
}
