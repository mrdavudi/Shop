using DiscountManagement.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contract.Product;

namespace ServiceHosts.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        public readonly IProductApplication _productApplication;
        public CustomerDiscountSearchModel SearchModel;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public SelectList products;

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(CustomerDiscountSearchModel SearchModel)
        {
            CustomerDiscounts = _customerDiscountApplication.Search(SearchModel);
            products = new SelectList(_productApplication.GetProduct(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProduct()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var DiscountDetails = _customerDiscountApplication.GetDetails(id);
            DiscountDetails.Products = _productApplication.GetProduct();
            return Partial("./Edit", DiscountDetails);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var DiscountDefine = _customerDiscountApplication.Edit(command);
            return new JsonResult(DiscountDefine);
        }
    }
}
