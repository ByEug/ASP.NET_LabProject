using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public int CartId { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

    }
}
