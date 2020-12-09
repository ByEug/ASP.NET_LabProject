using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LabProject.Models;
using LabProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace LabProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmailService _emailService;
        private readonly CustomLogger _logger;
        private readonly CartContext _cartContext;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, 
            EmailService emailService, CustomLogger logger, CartContext cartContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _logger = logger;
            _cartContext = cartContext;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    //EmailService emailService = new EmailService();
                    await _emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Confirm your email address: <a href='{callbackUrl}'>link</a>");

                    Cart new_cart = new Cart { UserId = user.Id };
                    _cartContext.Carts.Add(new_cart);
                    _cartContext.SaveChanges();

                    var Role = _roleManager.Roles.FirstOrDefault(o => o.Name == "user");

                    if (Role != null)
                    {
                        await _userManager.AddToRoleAsync(user, "user");
                    }

                    _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                    return Content("Follow the link from email message to finish your registration.");
                    //await _signInManager.SignInAsync(user, false);
                    //return RedirectToAction("Index", "Home");
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
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Email is not confirmed.");
                        return View(model);
                    }
                }

                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordMain(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
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
                        return RedirectToAction("Login");
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
