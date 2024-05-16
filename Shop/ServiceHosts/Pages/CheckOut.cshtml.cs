using _01_Query.Query.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHosts.Pages
{
    public class CheckOutModel : PageModel
    {
        private readonly ICheckOutCalculateService _checkOutCalculateService;
        public CheckOut CheckOutPage;
        public List<CartItem> CartItems;

        public CheckOutModel(ICheckOutCalculateService checkOutCalculateService)
        {
            _checkOutCalculateService = checkOutCalculateService;
        }

        public void OnGet()
        {
            var cookie = Request.Cookies["Cart-Item"];
            if (cookie != null)
                CartItems = JsonConvert.DeserializeObject<List<CartItem>>(cookie);

            if (CartItems != null)
                CheckOutPage = _checkOutCalculateService.CalculateDiscountForCheckOut(CartItems);
        }
    }
}
