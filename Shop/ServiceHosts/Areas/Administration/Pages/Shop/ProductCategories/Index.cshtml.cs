using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHosts.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication _productCategoryApplication;
        public List<ProductCategoryViewModel> ProductCategoryViewModel;
        public ProductCategorySearchModel SearchModel;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchmodel)
        {
            ProductCategoryViewModel = _productCategoryApplication.Search(searchmodel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var modelStateError = ModelState
                .Select(x => x.Value.Errors)
                .Where(x => x.Count > 0).Take(1);

            var productCategory = new OperationResult().Failed(modelStateError.ToString());
            if(ModelState.IsValid)
                productCategory = _productCategoryApplication.Create(command);
            
            return new JsonResult(productCategory);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication.GetDetail(id);
            return Partial("Edit", productCategory);
        }

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var modelStateError = ModelState
                .Select(x => x.Value.Errors)
                .Where(x => x.Count > 0).Take(1);

            var productCategoryEdit = new OperationResult().Failed(modelStateError.ToString());
            if (ModelState.IsValid)
                productCategoryEdit = _productCategoryApplication.Edit(command);

            return new JsonResult(productCategoryEdit);
        }
    }
}
