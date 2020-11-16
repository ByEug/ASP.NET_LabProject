using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Resources.Models
{
    public class BaseProduct
    {

        public string ModelName { get; set; }
        public int Price { get; set; }
        public string Discription { get; set; }
        public string ImageUrl { get; set; }

    }
}
