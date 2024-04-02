using _01_Query.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        public ProductQueryModel Product;
        public string value;

        public ProductDetailModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string id)
        {
            value = id;
            Product = _productQuery.GetProductDetail(id);
        }
    }
}
