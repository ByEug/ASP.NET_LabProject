using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Resources.Models
{
    public class Shoe : BaseProduct
    {
        public int Id { get; set; }

        public int Amount38 { get; set; }
        public int Amount39 { get; set; }
        public int Amount40 { get; set; }
        public int Amount41 { get; set; }
        public int Amount42 { get; set; }
        public int Amount43 { get; set; }
        public int Amount44 { get; set; }
        public int Amount45 { get; set; }

        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        public int UseWayId { get; set; }

        [ForeignKey("UseWayId")]
        public UseWay UseWay { get; set; }

    }
}
