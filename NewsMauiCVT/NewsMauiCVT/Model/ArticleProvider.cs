using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    internal class ArticleProvider
    {
        public int? Company_Id { get; set; }
        public int? ArticleProvider_Id { get; set; }
        public string Business_Partner_Id { get; set; }
        public int? Family_Id { get; set; }
        public int? MeasurementUnit_Id { get; set; }
        public int? ArticleType_Id { get; set; }
        public int? Accounts_Id { get; set; }
        public int? Container_Id { get; set; }
        public string ArticleProvider_CodClient { get; set; }
        public string ArticleProvider_Description { get; set; }
        public int? ArticleProvider_QualityStatus { get; set; }
        public int? ArticleProvider_ControlSN { get; set; }
        public int? ArticleProvider_ControlPN { get; set; }
        public int? ArticleProvider_ControlLT { get; set; }
        public int? ArticleProvider_AllowControlQuality { get; set; }
        public int? ArticleProvider_ExpiryTime { get; set; }
        public string ArticleProvider_Dato1 { get; set; }
        public float? ArticleProvider_GrossWeight { get; set; }
        public int? ArticleProvider_Bulk { get; set; }
        public int? ArticleProvider_CriticalStock { get; set; }
        public int? ArticleProvider_CriticalStockInPicking { get; set; }
        public string ArticleProvider_GTIN14 { get; set; }
        public int? ArticleProvider_Status { get; set; }
        public DateTime? ArticleProvider_CreationDate { get; set; }
        public int? ArticleProvider_UserCreationId { get; set; }
        public string INV_0010_SyncStat { get; set; }
        public string INV_0010_UpdFrom { get; set; }
        public DateTime? ArticleProvider_UpdateDate { get; set; }
        public int? ArticleProvider_UserUpdateId { get; set; }
        public string ArticleProvider_GTIN13 { get; set; }
        public int? ArticleProvider_EnvQty { get; set; }
        public float? ArticleProvider_Width { get; set; }
        public float? ArticleProvider_Height { get; set; }
        public float? ArticleProvider_Length { get; set; }
        public float? ArticleProvider_Volume { get; set; }
        public int? ArticleProvider_MaxStock { get; set; }
        public int? ArticleProvider_U_CantidadPaletizado { get; set; }
        public string ArticleProvider_U_NombreAlternativo { get; set; }
    }
}
