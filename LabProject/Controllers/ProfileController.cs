using LabProject.Models;
using LabProject.Services;
using LabProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CustomLogger _logger;
        private readonly CartContext _cartContext;

        public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager, 
            RoleManager<IdentityRole> roleManager, CustomLogger logger, CartContext cartContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _cartContext = cartContext;
        }

        public async Task<IActionResult> Profile(string name)
        {
            User user = await _userManager.FindByNameAsync(name);
            var userRoles = await _userManager.GetRolesAsync(user);
            bool management = false;
            if (userRoles.Contains("admin") && userRoles.Contains("moderator"))
            {
                management = true;
            }
            ViewBag.management = management;

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View(user);
        }

        public IActionResult Cart()
        {
            int CartId = _cartContext.Carts.Single(o => o.UserId == _userManager.GetUserId(User)).Id;
            List<CartItem> listItem = _cartContext.CartItems.Where(o => o.CartId == CartId).ToList();
            return View(listItem);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Year = user.Year };

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Year = model.Year;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                        return RedirectToAction("Profile", new { name = user.UserName });
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
            }
            return View(model);
        }


    }
}