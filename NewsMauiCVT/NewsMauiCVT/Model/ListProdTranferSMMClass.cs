using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    public class ListProdTranferSMMClass
    {
        public string Package_SSCC { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public float Package_Quantity { get; set; }
        public string Package_Lot { get; set; }
        public DateTime FechaProduccion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Site_Description { get; set; }
        public int Site_Id { get; set; }
        public int Layout_Id { get; set; }
        public int Package_Id { get; set; }
    }
}
