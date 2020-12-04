using LabProject.Models;
using LabProject.Resources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public HomeController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            /*List<BaseProduct> products = new List<BaseProduct>();
            products.AddRange(_context.Shoes.ToList());
            products.AddRange(_context.WearProducts.ToList());
            return View(products);*/
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Id = _userManager.GetUserId(User);
            }
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.UseWays = _context.UseWays.ToList();
            IEnumerable<BaseProduct> products = _context.Shoes.Include(current => current.Brand).Include(current => current.UseWay).ToList();
            products.Concat(_context.WearProducts.Include(current => current.Brand).Include(current => current.UseWay).Cast<BaseProduct>().ToArray());
            return View(products);
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
            return View(product);
        }
    }
}
