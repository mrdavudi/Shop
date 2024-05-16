using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Repository.Permissions;

namespace ShopManagement.Configuration
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDTO>> Expose()
        {
            return new Dictionary<string, List<PermissionDTO>>
            {
                {
                    "Product", new List<PermissionDTO>
                    {
                        new PermissionDTO(ShopPermissions.ListProducts, "ListProducts"),
                        new PermissionDTO(ShopPermissions.SearchProducts, "SearchProduct"),
                        new PermissionDTO(ShopPermissions.createProducts, "CreateProduct"),
                        new PermissionDTO(ShopPermissions.EditProducts, "EditProduct")
                    }
                },
                {
                    "ProductCategory", new List<PermissionDTO>
                    {
                        new PermissionDTO(ShopPermissions.SearchProductCategories, "SearchProductCategories"),
                        new PermissionDTO(ShopPermissions.ListProductCategories, "ListProductCategories"),
                        new PermissionDTO(ShopPermissions.CreateProductCategories, "CreateProductCategories"),
                        new PermissionDTO(ShopPermissions.EditProductCategories, "EditProductCategories"),
                    }
                }
            };
        }
    }
}
