using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Resources.Models
{
    public class WearProduct: BaseProduct
    {
        public int Id { get; set; }

        public int AmountS { get; set; }
        public int AmountM { get; set; }
        public int AmountL { get; set; }
        public int AmountXL { get; set; }
        public int AmountXXL { get; set; }

        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        public int UseWayId { get; set; }

        [ForeignKey("UseWayId")]
        public UseWay UseWay { get; set; }

    }
}
