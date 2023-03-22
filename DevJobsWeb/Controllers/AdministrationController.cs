using Contracts;
using DevJobsWeb.Areas.Identity.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevJobsWeb.Controllers
{
    [AllowAnonymous]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<DevJobsWebUser> userManager;
        private readonly IRepositoryWrapper _repository;

        public AdministrationController(IRepositoryWrapper repository, RoleManager<IdentityRole> roleManager, UserManager<DevJobsWebUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _repository = repository;
        }

        //[Route("Administration/Create/{Id}")]
                              
        public IActionResult Index()
          {
            var role = this.roleManager.Roles.ToList();
            return View(role);
        }

        [HttpGet]
        [Route("Administration/CreateRole")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);

        }

        public IActionResult UserList()
        {
            var users = userManager.Users;
          //  var roles = roleManager.Roles;
            return View(users);
        }

        [HttpGet]
       // [Route("Administration/EditRole")]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);            

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name

            };

            foreach (var user in userManager.Users.ToList())
            {
                
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
                return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }                  
            else
            {                                                  
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        { 
             ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId); 

            if(role == null)
            {

                ViewBag.ErrorMessage = $"Role with Id ={roleId} can not be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach(var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if(await userManager.IsInRoleAsync(user, role.Name))      // this is where it crushes
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                { 
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }


        public async Task<IActionResult> EditUsersInRole (List<UserRoleViewModel> model, string roleId)
        {
             var role = await roleManager.FindByIdAsync(roleId);    
            if(role == null)
            {

                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View();
            }
            for(int i=0; i<model.Count;i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))    ///breaks here
                {
                    result = await userManager.RemoveFromRoleAsync( user, role.Name);
                }
                else
                {
                    continue;
                }

                if(result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }

            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpPost]
        [Route("Administration/DeleteRole/{Id}")]
        public async Task<IActionResult> DeleteRole(string id)            //when deletetig a rolethe break poin it supposed to be hit
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id ={id} cannnot be found";
                return View("NotFound");
            }                                         
            else 
            {
                var result = await roleManager.DeleteAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }


}
