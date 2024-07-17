using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    public class SMMProductosEnOC
    {
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public DateTime fecha { get; set; }
        public string proveedor { get; set; }
        public string NomProveedor { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public float Quantity { get; set; }
        public string CodeBars { get; set; }
        public string DocStatus { get; set; }
        public string SKUProveedor { get; set; }
        public int Price { get; set; }

    }
}
