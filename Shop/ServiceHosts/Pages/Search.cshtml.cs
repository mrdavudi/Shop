using _01_Query.Contract.Product;
using _01_Query.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        public List<ProductQueryModel> products;
        public string Value;

        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string value)
        {
            Value = value;
            products = _productQuery.Search(value);
        }
    }
}
