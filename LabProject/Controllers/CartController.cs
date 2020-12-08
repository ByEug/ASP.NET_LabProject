using LabProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Controllers
{
    public class CartController: Controller
    {
        private readonly CartContext _cartContext;
        private readonly DataContext _context;

        public CartController(CartContext cartContext, DataContext context)
        {
            _cartContext = cartContext;
            _context = context;
        }
    }
}
