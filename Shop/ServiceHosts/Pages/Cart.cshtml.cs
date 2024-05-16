using _01_Query.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopManagement.Application.Contract.Order;

namespace ServiceHosts.Pages
{
    public class CartModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        public List<CartItem>? CartItems;
        public string CookieName = "Cart-Item";

        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var Cookie = Request.Cookies[CookieName];
            if(Cookie == null)
                return;

            try
            {
                var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(Cookie);
                CartItems = _productQuery.CheckCartItemsStatus(cartItems);
            }
            catch
            {
                CartItems = null;
                Response.Cookies.Delete(CookieName);
            }
        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            var Cookie = Request.Cookies[CookieName];
            if(Cookie == null)
                return RedirectToPage("/Cart");

            List<CartItem> DeserializedCookieValue;
            try
            {
                DeserializedCookieValue = JsonConvert.DeserializeObject<List<CartItem>>(Cookie);
                Response.Cookies.Delete(CookieName);

                var CookieItemToRemove = DeserializedCookieValue.Find(x => x.Id == id);
                DeserializedCookieValue.Remove(CookieItemToRemove);

                if (DeserializedCookieValue.Count != 0)
                {
                    var CookieOptions = new CookieOptions
                    {
                        Path = "/",
                        IsEssential = true,
                        Expires = DateTime.UtcNow.AddDays(1)
                    };

                    Response.Cookies.Append(CookieName, JsonConvert.SerializeObject(DeserializedCookieValue),
                        CookieOptions);
                }

                return RedirectToPage("/Cart");
            }
            catch
            {
                return RedirectToPage("/Cart");
            }
        }

        public IActionResult OnGetGoToCheckOut()
        {
            var cookie = Request.Cookies[CookieName];
            if (cookie != null)
            {
                var deserializeObject = JsonConvert.DeserializeObject<List<CartItem>>(cookie);
                CartItems = _productQuery.CheckCartItemsStatus(deserializeObject);

                if (CartItems.Any(x => !x.IsInStock))
                    return RedirectToPage("/Cart");

                return RedirectToPage("/CheckOut");
            }

            return RedirectToPage("/Cart");
        }
    }
}
