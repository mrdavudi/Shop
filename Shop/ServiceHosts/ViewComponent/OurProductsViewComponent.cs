using _01_Query.Contract.ProductCategoryQuery;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHosts.ViewComponent
{
    public class OurProductsViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public OurProductsViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productsWithCategories = _productCategoryQuery.GetProductCategoriesWithProducts();
            return View(productsWithCategories);
        }
    }
}
