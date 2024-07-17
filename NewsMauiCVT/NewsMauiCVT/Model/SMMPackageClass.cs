using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    public class SMMPackageClass
    {
        public int Package_Id { get; set; }
        public int? Reception_Id { get; set; }
        public int Layout_Id { get; set; }
        public string ItemCode { get; set; }
        public float Package_QuantityInitial { get; set; }
        public float Package_Quantity { get; set; }
        public int Package_Status { get; set; }
        public string Package_SSCC { get; set; }
        public string Package_Lot { get; set; }
        public DateTime Package_ProductionDate { get; set; }
        public DateTime Package_ExpiresDate { get; set; }
        public DateTime Package_InDate { get; set; }
        public DateTime? Package_OutDate { get; set; }
        public string Package_SN { get; set; }
        public float Package_ReserveQuantity { get; set; }
        public string Package_Data1 { get; set; }
        public string Package_Data2 { get; set; }
        public int? Linea { get; set; }
    }
}
