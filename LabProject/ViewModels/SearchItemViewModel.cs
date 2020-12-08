using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.ViewModels
{
    public class SearchItemViewModel
    {
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string UseWayName { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
