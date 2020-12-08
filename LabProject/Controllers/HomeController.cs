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

namespace LabProject.Resources.Controllers
{
    public class HomeController: Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly CustomLogger _logger;

        public HomeController(DataContext context, UserManager<User> userManager, CustomLogger customLogger)
        {
            _context = context;
            _userManager = userManager;
            _logger = customLogger;
        }

        public IActionResult Index()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Id = _userManager.GetUserId(User);
            }
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.UseWays = _context.UseWays.ToList();
            ViewBag.Shoes = _context.Shoes.Include(current => current.Brand).Include(current => current.UseWay).ToList();

            SearchItemViewModel viewModel = new SearchItemViewModel();

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(SearchItemViewModel viewModel)
        {
            /*if(viewModel.ProductName != "")
            {
                List<Shoe> list = new List<Shoe>();
                list.Add(_context.Shoes.Include(current => current.Brand).Include(current => current.UseWay)
                    .Where(o => o.ModelName == viewModel.ProductName).FirstOrDefault());
                ViewBag.Shoes = list;
            }*/

            List<Shoe> list = new List<Shoe>();
            list.AddRange(_context.Shoes.Include(current => current.Brand).Include(current => current.UseWay)
                .Where(o => (o.Brand.BrandName == viewModel.BrandName || viewModel.BrandName == "-") &&
                (o.UseWay.WayName == viewModel.UseWayName || viewModel.UseWayName == "-") &&
                (EF.Functions.Like(o.ModelName, $"%{viewModel.ProductName}%") || viewModel.ProductName == "") &&
                (o.Price >= viewModel.MinPrice && o.Price <= viewModel.MaxPrice))
                .ToList());

            ViewBag.Shoes = list;

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Id = _userManager.GetUserId(User);
            }
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.UseWays = _context.UseWays.ToList();

            return View("Index");

        }


        public IActionResult Clothes()
        {

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Id = _userManager.GetUserId(User);
            }
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.UseWays = _context.UseWays.ToList();

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View(_context.WearProducts
                .Include(current => current.Brand)
                .Include(current => current.UseWay)
                .ToList());
        }

        public IActionResult Error(string code)
        {
            ViewBag.errorNum = code;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Show(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.itemId = id;
            var product = _context.Shoes
                .Include(current => current.Brand)
                .Include(current => current.UseWay)
                .Single(current => current.Id == id);

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View(product);
        }

        [HttpGet]
        public IActionResult ShowClothes(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.itemId = id;
            var product = _context.WearProducts
                .Include(current => current.Brand)
                .Include(current => current.UseWay)
                .Single(current => current.Id == id);

            _logger.LogInformation($"Processing request {this.Request.Path} at {DateTime.Now:hh:mm:ss}");
            return View(product);
        }
    }
}
