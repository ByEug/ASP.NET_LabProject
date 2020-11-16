using LabProject.Models;
using LabProject.Resources.Models;
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

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            /*List<BaseProduct> products = new List<BaseProduct>();
            products.AddRange(_context.Shoes.ToList());
            products.AddRange(_context.WearProducts.ToList());
            return View(products);*/
            return View(_context.Shoes
                .Include(current => current.Brand)
                .Include(current => current.UseWay)
                .ToList());
        }
    }
}
