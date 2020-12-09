using LabProject.Models;
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
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly CustomLogger _logger;
        private readonly CartContext _cartContext;

        public UsersController(UserManager<User> userManager, CustomLogger logger, CartContext cartContext)
        {
            _userManager = userManager;
            _logger = logger;
            _cartContext = cartContext;
        }

        public async Task<IActionResult> Index() {

            User user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if (user != null)
            {
                var Roles = await _userManager.GetRolesAsync(user);
                if (Roles.Contains("admin") || Roles.Contains("moderator"))
                {
                    _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                    return View(_userManager.Users.ToList());
                }
                else
                {
                    _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                    return NotFound();
                }
            }
            _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return NotFound();

        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
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
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                Cart del_cart = _cartContext.Carts.FirstOrDefault(o => o.UserId == id);

                if (del_cart != null)
                {
                    _cartContext.Carts.Remove(del_cart);
                    _cartContext.SaveChanges();
                }

                IdentityResult result = await _userManager.DeleteAsync(user);
            }

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
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
                else
                {
                    _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                    ModelState.AddModelError(string.Empty, "User did not found");
                }
            }
            return View(model);
        }
    }
}
