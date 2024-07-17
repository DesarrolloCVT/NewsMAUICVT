using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    internal class LayoutClass
    {
        public int Layout_Id { get; set; }
        public int Site_Id { get; set; }
        public int Warehouse_Id { get; set; }
        public int Sector_Id { get; set; }
        public string Layout_Description { get; set; }
        public string Layout_ShortDescription { get; set; }
        public int Layout_Status { get; set; }

    }
}
