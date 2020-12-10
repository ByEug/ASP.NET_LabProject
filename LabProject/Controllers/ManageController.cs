using LabProject.Models;
using LabProject.Resources.Models;
using LabProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Controllers
{
    public class ManageController: Controller
    {
        private readonly CartContext _cartContext;
        private readonly DataContext _context;
        private readonly CustomLogger _logger;
        private readonly UserManager<User> _userManager;

        public ManageController(CartContext cartContext, DataContext context, CustomLogger logger, UserManager<User> userManager)
        {
            _cartContext = cartContext;
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AddShoes()
        {

            if (await CheckRoles())
            {
                _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                ViewBag.Brands = _context.Brands.ToList();
                ViewBag.UseWays = _context.UseWays.ToList();
                AddShoeViewModel viewModel = new AddShoeViewModel();
                return View(viewModel);
            }
            _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddShoes(AddShoeViewModel viewModel)
        {
            if (viewModel.ImageUrl == null || viewModel.ProductName == null)
            {
                AddShoeViewModel model = new AddShoeViewModel();
                return RedirectToAction("AddShoes", new { viewModel = model });
            }
            else
            {
                Shoe shoe = new Shoe {
                    ModelName = viewModel.ProductName,
                    Price = viewModel.Price,
                    Discription = viewModel.Discription,
                    ImageUrl = viewModel.ImageUrl,
                    BrandId = _context.Brands.Single(o => o.BrandName == viewModel.Brand).Id,
                    UseWayId = _context.UseWays.Single(o => o.WayName == viewModel.UseWay).Id,
                    Amount38 = viewModel.Amount38,
                    Amount39 = viewModel.Amount39,
                    Amount40 = viewModel.Amount40,
                    Amount41 = viewModel.Amount41,
                    Amount42 = viewModel.Amount42,
                    Amount43 = viewModel.Amount43,
                    Amount44 = viewModel.Amount44,
                    Amount45 = viewModel.Amount45
                };

                _context.Shoes.Add(shoe);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteShoes(string message = null)
        {
            if (await CheckRoles())
            {
                _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                ViewBag.Shoes = _context.Shoes.Include(current => current.Brand).Include(current => current.UseWay).ToList();
                ViewBag.Message = message;
                return View();
            }
            _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return NotFound();
        }

        [HttpPost]
        public IActionResult DeleteCurrentShoes(int? id, string name)
        {
            if (_cartContext.CartItems.FirstOrDefault(o => o.IdInBase == id && o.ItemName == name) != null)
            {
                string mess = "This product exists in users' carts.";
                return RedirectToAction("DeleteShoes", new { message = mess });
            }
            else
            {
                Shoe delete_this = _context.Shoes.FirstOrDefault(o => o.Id == id);
                _context.Shoes.Remove(delete_this);
                _context.SaveChanges();
                return RedirectToAction("DeleteShoes");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddWear()
        {
            if (await CheckRoles())
            {
                _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                ViewBag.Brands = _context.Brands.ToList();
                ViewBag.UseWays = _context.UseWays.ToList();
                AddWearViewModel viewModel = new AddWearViewModel();
                return View(viewModel);
            }
            _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddWear(AddWearViewModel viewModel)
        {
            if (viewModel.ImageUrl == null || viewModel.ProductName == null)
            {
                AddWearViewModel model = new AddWearViewModel();
                return RedirectToAction("AddWear", new { viewModel = model });
            }
            else
            {
                WearProduct wear = new WearProduct
                {
                    ModelName = viewModel.ProductName,
                    Price = viewModel.Price,
                    Discription = viewModel.Discription,
                    ImageUrl = viewModel.ImageUrl,
                    BrandId = _context.Brands.Single(o => o.BrandName == viewModel.Brand).Id,
                    UseWayId = _context.UseWays.Single(o => o.WayName == viewModel.UseWay).Id,
                    AmountS = viewModel.AmountS,
                    AmountM = viewModel.AmountM,
                    AmountL = viewModel.AmountL,
                    AmountXL = viewModel.AmountXL,
                    AmountXXL = viewModel.AmountXXL
                };

                _context.WearProducts.Add(wear);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteWear(string message = null)
        {
            if (await CheckRoles())
            {
                _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
                ViewBag.WearProducts = _context.WearProducts.Include(current => current.Brand).Include(current => current.UseWay).ToList();
                ViewBag.Message = message;
                return View();
            }
            _logger.LogError($"Error in {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return NotFound();
        }

        [HttpPost]
        public IActionResult DeleteCurrentWear(int? id, string name)
        {
            if (_cartContext.CartItems.FirstOrDefault(o => o.IdInBase == id && o.ItemName == name) != null)
            {
                string mess = "This product exists in users' carts.";
                return RedirectToAction("DeleteWear", new { message = mess });
            }
            else
            {
                WearProduct delete_this = _context.WearProducts.FirstOrDefault(o => o.Id == id);
                _context.WearProducts.Remove(delete_this);
                _context.SaveChanges();
                return RedirectToAction("DeleteWear");
            }
        }

        private async Task<bool> CheckRoles()
        {
            User user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if (user != null)
            {
                var Roles = await _userManager.GetRolesAsync(user);
                if (Roles.Contains("admin") || Roles.Contains("moderator"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
