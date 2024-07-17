using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    internal class Package
    {
        public int? Company_Id { get; set; }
        public int? Package_Id { get; set; }
        public int? Supportive_Id { get; set; }
        public int? Reception_Id { get; set; }
        public int? Layout_Id { get; set; }
        public int? ArticleProvider_Id { get; set; }
        public float? ArticleProvider_GrossWeight { get; set; }
        public string Package_Barcode { get; set; }
        public int? Package_QuantityInitial { get; set; }
        public int? Package_Quantity { get; set; }
        public float? Package_NetWeightInitial { get; set; }
        public float? Package_NetWeight { get; set; }
        public float? Package_GrossWeightInitial { get; set; }
        public float? Package_GrossWeight { get; set; }
        public float? Package_Height { get; set; }
        public int? Package_Status { get; set; }
        public int? Package_QualityStatus { get; set; }
        public string Package_Owner { get; set; }
        public string Package_SSCC { get; set; }
        public string Package_SSCCParent { get; set; }
        public int? Package_SSCCParent_Id { get; set; }
        public string Package_Lot { get; set; }
        public DateTime? Package_ProductionDate { get; set; }
        public DateTime? Package_ExpiresDate { get; set; }
        public int? Package_Mixed { get; set; }
        public DateTime? Package_InDate { get; set; }
        public DateTime? Package_OutDate { get; set; }
        public string Package_SN { get; set; }
        public string Package_PN { get; set; }
        public int? Package_ReserveQuantity { get; set; }
        public int? Package_Reserve { get; set; }
        public string Package_Data1 { get; set; }
        public string Package_Data2 { get; set; }
        public int? PackageState_Id { get; set; }
        public int? Accounts_Id { get; set; }
        public string Package_FoilPP { get; set; }
        public int? EntryType { get; set; }
        public int? Package_Origin { get; set; }
        public int? Package_Destination { get; set; }
        public int? Package_DependencesTotal { get; set; }
        public int? Package_CrossDocking { get; set; }
        public int? PackageState_Id_Initial { get; set; }
        public string DevolucionMotivo_Id { get; set; }

    }
}
