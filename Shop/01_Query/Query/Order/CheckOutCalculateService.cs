using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application.Auth;
using _0_Framework.Repository;
using DiscountManagement.Infrastructure;
using ShopManagement.Application.Contract.Order;

namespace _01_Query.Query.Order
{
    public class CheckOutCalculateService : ICheckOutCalculateService
    {
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;
        public CheckOut checkOut;

        public CheckOutCalculateService(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
            checkOut = new CheckOut();
        }

        public CheckOut CalculateDiscountForCheckOut(List<CartItem> cartItems)
        {
            var currentUserRole = _authHelper.CurrentAccountRole();
            foreach (var cartItem in cartItems)
            {
                if (currentUserRole == Roles.ColleagueUser)
                {
                    var colleagueDiscount =
                        _discountContext.ColleagueDiscount.FirstOrDefault(x => x.ProductId == cartItem.Id);

                    if (colleagueDiscount != null)
                        cartItem.DiscountRate = colleagueDiscount.DiscountRate;
                }
                else
                {
                    var customerDiscount =
                        _discountContext.CustomerDiscount.FirstOrDefault(x => x.ProductId == cartItem.Id);

                    if (customerDiscount != null)
                        cartItem.DiscountRate = customerDiscount.DiscountRate;
                }

                if(cartItem.DiscountRate > 0)
                    cartItem.DiscountAmount = ((cartItem.TotalPrice * cartItem.DiscountRate) / 100);

                cartItem.ItemPayAmount = cartItem.TotalPrice - cartItem.DiscountAmount;
                checkOut.Add(cartItem);
            }

            return checkOut;
        }
    }
}
