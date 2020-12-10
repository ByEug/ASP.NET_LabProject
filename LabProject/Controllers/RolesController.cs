using LabProject.Models;
using LabProject.Services;
using LabProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<RolesHub> _hubContext;
        private readonly CustomLogger _logger;
        private readonly TimeService _timeService;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager,
            IHubContext<RolesHub> hubContext, CustomLogger logger, TimeService timeService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _hubContext = hubContext;
            _logger = logger;
            _timeService = timeService;
        }

        public async Task<IActionResult> Index() {

            User user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if (user != null)
            {
                var Roles = await _userManager.GetRolesAsync(user);
                if (Roles.Contains("admin") || Roles.Contains("moderator"))
                {
                    _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                    return View(_roleManager.Roles.ToList());
                }
            }
            _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return NotFound();
        }

        public IActionResult Create() {

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    await _hubContext.Clients.All.SendAsync("ShowMessageCreate", " role was created successfully");

                    _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                    return RedirectToAction("Index");
                }
                else
                {
                    _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            await _hubContext.Clients.All.SendAsync("ShowMessage", "Role was deleted successfully at " + _timeService.Time);

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return RedirectToAction("Index");
        }

        public IActionResult UserList() {

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View(_userManager.Users.ToList());
        }

        public async Task<IActionResult> Edit(string userId)
        {
            
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                
                var userRoles = await _userManager.GetRolesAsync(user);
                
                var allRoles = _roleManager.Roles.ToList();
                
                var addedRoles = roles.Except(userRoles);
                
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                return RedirectToAction("UserList");
            }

            _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return NotFound();
        }
    }
}
