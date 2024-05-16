using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contract.Order
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public int Count { get; set; }
        public string Slug { get; set; }
        public double TotalPrice { get; set; }
        public bool IsInStock { get; set; }
        public int DiscountRate { get; set; }
        public double DiscountAmount { get; set; }
        public double ItemPayAmount { get; set; }

        public CartItem(int id, string name, long price, string picture, int count, string slug)
        {
            Id = id;
            Name = name;
            Price = price;
            Picture = picture;
            Count = count;
            Slug = slug;
            TotalPrice = price * count;
        }
    }
}
