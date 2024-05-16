using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Application.Contract.Order;

namespace _01_Query.Query.Order
{
    public interface ICheckOutCalculateService
    {
        public CheckOut CalculateDiscountForCheckOut(List<CartItem> cartItems);
    }
}
