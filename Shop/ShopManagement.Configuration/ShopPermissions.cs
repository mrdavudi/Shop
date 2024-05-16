using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Configuration
{
    public static class ShopPermissions
    {
        //Products
        public const int ListProducts = 10;
        public const int SearchProducts = 11;
        public const int createProducts = 12;
        public const int EditProducts = 13;

        //Products Category
        public const int ListProductCategories = 20;
        public const int SearchProductCategories = 21;
        public const int CreateProductCategories = 22;
        public const int EditProductCategories = 23;
    }
}
