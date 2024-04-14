using _01_Query.Contract.ProductCategoryQuery;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class ProductCategoryDetailModel : PageModel
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        public ProductCategoryQueryModel productCategory;
        public string Value;

        public ProductCategoryDetailModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(string id)
        {
            Value = id;
            productCategory = _productCategoryQuery.GetProductCategoriesWithProductBy(id);
        }
    }
}
