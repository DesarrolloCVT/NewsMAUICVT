using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    public class SMMDatoProductosRecepcion
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UgpName { get; set; }
        public float BaseQty { get; set; }
        public int UgpEntry { get; set; }
        public string BcdCode { get; set; }
        public int UomEntry { get; set; }
        public string UomCode { get; set; }

    }
}
