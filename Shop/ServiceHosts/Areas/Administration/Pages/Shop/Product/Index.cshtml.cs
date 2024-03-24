using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ServiceHosts.Areas.Administration.Pages.Shop.Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public SelectList productCategories;
        public List<productViewModel> Products;
        public productSearchModel SearchModel;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(productSearchModel searchModel)
        {
            productCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            Products = _productApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                categories = _productCategoryApplication.GetProductCategories()
            };

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var newProduct = _productApplication.Create(command);
            return new JsonResult(newProduct);
        }

        public IActionResult OnGetEdit(long id)
        {
            var editProduct = _productApplication.GetDetails(id);
            editProduct.categories = _productCategoryApplication.GetProductCategories();
            return Partial("Edit", editProduct);
        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            var editProduct = _productApplication.Edit(command);
            return new JsonResult(editProduct);
        }
    }
}
