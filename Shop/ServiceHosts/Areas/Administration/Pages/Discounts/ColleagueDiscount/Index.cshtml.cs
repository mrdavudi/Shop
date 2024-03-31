using System.Dynamic;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;

namespace ServiceHosts.Areas.Administration.Pages.Discounts.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;
        public List<ColleagueDiscountViewModel> ColleagueDiscount;
        public SelectList products;
        public ColleagueDiscountSearchModel SearchModel;


        public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication,
            IProductApplication productApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel SearchModel)
        {
            products = new SelectList(_productApplication.GetProduct(), "Id", "Name");
            ColleagueDiscount = _colleagueDiscountApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = _productApplication.GetProduct()
            };
            
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var DefineDiscount = _colleagueDiscountApplication.Define(command);
            return new JsonResult(DefineDiscount);
        }

        public IActionResult OnGetEdit(long id)
        {
            var EditDiscount = _colleagueDiscountApplication.GetDetails(id);
            EditDiscount.Products = _productApplication.GetProduct();

            return Partial("./Edit", EditDiscount);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var EditDiscount = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(EditDiscount);
        }

        public IActionResult OnGetRemove(long id)
        {
            _colleagueDiscountApplication.Remove(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            _colleagueDiscountApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
