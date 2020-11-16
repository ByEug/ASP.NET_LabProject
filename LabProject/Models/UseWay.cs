using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Resources.Models
{
    public class UseWay
    {
        public int Id { get; set; }
        public string WayName { get; set; }

        public List<WearProduct> WearProducts { get; set; }
        public List<Shoe> Shoes { get; set; }

    }
}
