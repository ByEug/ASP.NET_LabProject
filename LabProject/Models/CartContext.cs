using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Models
{
    public class CartContext: DbContext
    {
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public CartContext(DbContextOptions<CartContext> options)
            : base(options)
        {

        }
    }
}
