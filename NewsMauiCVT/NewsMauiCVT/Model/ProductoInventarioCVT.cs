using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    public class ProductoInventarioCVT
    {

        public string Package_SSCC { get; set; }
        public int Layout_Id { get; set; }
        public int Site_Id { get; set; }
        public string Package_Lot { get; set; }
        public string ArticleProvider_Description { get; set; }
        public string ArticleProvider_CodClient { get; set; }
        public int ArticleProvider_Id { get; set; }
    }
}
