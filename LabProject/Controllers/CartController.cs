using LabProject.Models;
using LabProject.Resources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> _userManager;

        public CartController(CartContext cartContext, DataContext context, UserManager<User> userManager)
        {
            _cartContext = cartContext;
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult BuyShoe(string size, int? shoeid)
        {
            Shoe product = _context.Shoes
                .Include(current => current.Brand)
                .Include(current => current.UseWay)
                .Single(current => current.Id == shoeid);

            switch (size)
            {
                case "38":
                    {
                        if (product.Amount38 == 0)
                        {
                            return RedirectToAction("Show", "Home", new { id = shoeid });
                        }
                        else
                        {
                            _context.Shoes.Single(current => current.Id == shoeid).Amount38 =
                                _context.Shoes.Single(current => current.Id == shoeid).Amount38 - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "39":
                    {
                        if (product.Amount39 == 0)
                        {
                            return RedirectToAction("Show", "Home", new { id = shoeid });
                        }
                        else
                        {
                            _context.Shoes.Single(current => current.Id == shoeid).Amount39 =
                                _context.Shoes.Single(current => current.Id == shoeid).Amount39 - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "40":
                    {
                        if (product.Amount40 == 0)
                        {
                            return RedirectToAction("Show", "Home", new { id = shoeid });
                        }
                        else
                        {
                            _context.Shoes.Single(current => current.Id == shoeid).Amount40 =
                                _context.Shoes.Single(current => current.Id == shoeid).Amount40 - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "41":
                    {
                        if (product.Amount41 == 0)
                        {
                            return RedirectToAction("Show", "Home", new { id = shoeid });
                        }
                        else
                        {
                            _context.Shoes.Single(current => current.Id == shoeid).Amount41 =
                                _context.Shoes.Single(current => current.Id == shoeid).Amount41 - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "42":
                    {
                        if (product.Amount42 == 0)
                        {
                            return RedirectToAction("Show", "Home", new { id = shoeid });
                        }
                        else
                        {
                            _context.Shoes.Single(current => current.Id == shoeid).Amount42 =
                                _context.Shoes.Single(current => current.Id == shoeid).Amount42 - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "43":
                    {
                        if (product.Amount43 == 0)
                        {
                            return RedirectToAction("Show", "Home", new { id = shoeid });
                        }
                        else
                        {
                            _context.Shoes.Single(current => current.Id == shoeid).Amount43 =
                                _context.Shoes.Single(current => current.Id == shoeid).Amount43 - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "44":
                    {
                        if (product.Amount44 == 0)
                        {
                            return RedirectToAction("Show", "Home", new { id = shoeid });
                        }
                        else
                        {
                            _context.Shoes.Single(current => current.Id == shoeid).Amount44 =
                                _context.Shoes.Single(current => current.Id == shoeid).Amount44 - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "45":
                    {
                        if (product.Amount45 == 0)
                        {
                            return RedirectToAction("Show", "Home", new { id = shoeid });
                        }
                        else
                        {
                            _context.Shoes.Single(current => current.Id == shoeid).Amount45 =
                                _context.Shoes.Single(current => current.Id == shoeid).Amount45 - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                default:
                    {
                        return RedirectToAction("Show", "Home", new { id = shoeid });
                    }

            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult BuyWear(string size, int? wearid)
        {
            WearProduct product = _context.WearProducts
                .Include(current => current.Brand)
                .Include(current => current.UseWay)
                .Single(current => current.Id == wearid);

            switch (size)
            {
                case "S":
                    {
                        if (product.AmountS == 0)
                        {
                            return RedirectToAction("ShowClothes", "Home", new { id = wearid });
                        }
                        else
                        {
                            _context.WearProducts.Single(current => current.Id == wearid).AmountS =
                                _context.WearProducts.Single(current => current.Id == wearid).AmountS - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "M":
                    {
                        if (product.AmountM == 0)
                        {
                            return RedirectToAction("ShowClothes", "Home", new { id = wearid });
                        }
                        else
                        {
                            _context.WearProducts.Single(current => current.Id == wearid).AmountM =
                                _context.WearProducts.Single(current => current.Id == wearid).AmountM - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "L":
                    {
                        if (product.AmountL == 0)
                        {
                            return RedirectToAction("ShowClothes", "Home", new { id = wearid });
                        }
                        else
                        {
                            _context.WearProducts.Single(current => current.Id == wearid).AmountL =
                                _context.WearProducts.Single(current => current.Id == wearid).AmountL - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "XL":
                    {
                        if (product.AmountXL == 0)
                        {
                            return RedirectToAction("ShowClothes", "Home", new { id = wearid });
                        }
                        else
                        {
                            _context.WearProducts.Single(current => current.Id == wearid).AmountXL =
                                _context.WearProducts.Single(current => current.Id == wearid).AmountXL - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                case "XXL":
                    {
                        if (product.AmountXXL == 0)
                        {
                            return RedirectToAction("ShowClothes", "Home", new { id = wearid });
                        }
                        else
                        {
                            _context.WearProducts.Single(current => current.Id == wearid).AmountXXL =
                                _context.WearProducts.Single(current => current.Id == wearid).AmountXXL - 1;
                            _context.SaveChanges();
                            AddToCart(product, size);
                        }
                        break;
                    }
                default:
                    {
                        return RedirectToAction("ShowClothes", "Home", new { id = wearid });
                    }

            }
            return RedirectToAction("Clothes", "Home");

        }

        private void AddToCart(Shoe product, string size)
        {
            CartItem cart_item = new CartItem
            {
                Brand = product.Brand.BrandName,
                IdInBase = product.Id,
                ImageUrl = product.ImageUrl,
                ItemName = product.ModelName,
                Price = product.Price,
                Size = size,
                CartId = _cartContext.Carts
                .Single(o => o.UserId == _userManager.GetUserId(User)).Id
            };

            _cartContext.CartItems.Add(cart_item);
            _cartContext.SaveChanges();
        }

        private void AddToCart(WearProduct product, string size)
        {
            CartItem cart_item = new CartItem
            {
                Brand = product.Brand.BrandName,
                IdInBase = product.Id,
                ImageUrl = product.ImageUrl,
                ItemName = product.ModelName,
                Price = product.Price,
                Size = size,
                CartId = _cartContext.Carts
                .Single(o => o.UserId == _userManager.GetUserId(User)).Id
            };

            _cartContext.CartItems.Add(cart_item);
            _cartContext.SaveChanges();
        }
    }
}
