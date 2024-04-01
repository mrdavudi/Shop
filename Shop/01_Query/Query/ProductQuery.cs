using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_Query.Contract.Product;
using DiscountManagement.Infrastructure;
using InventoryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure;

namespace _01_Query.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly DiscountContext _discountContext;
        private readonly InventoryContext _inventoryContext;

        public ProductQuery(ShopContext shopContext, DiscountContext discountContext, InventoryContext inventoryContext)
        {
            _shopContext = shopContext;
            _discountContext = discountContext;
            _inventoryContext = inventoryContext;
        }

        public List<ProductQueryModel> GeLatestArrivals()
        {
            var Inventory = _inventoryContext.Inventory.Select(x => new { x.UnitPrice, x.ProductId }).ToList();
            var Discount = _discountContext.CustomerDiscount
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();

            var productList = _shopContext.Products
                .Include(x => x.Category)
                .ThenInclude(x => x.Products)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Name = x.Name,
                    Category = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    Code = x.Code,
                    Slug = x.Slug,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription
                }).AsNoTracking().OrderByDescending(x=>x.Id).Take(6).ToList();

            foreach (var product in productList)
            {
                var productInventory = Inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var productDiscount = Discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                        var discountRate = productDiscount.DiscountRate;
                        var discountPrice = Math.Round(price * discountRate) / 100;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        product.PriceWithDiscount = (price - discountPrice).ToMoney();
                    }
                }
            }
            
            return productList;
        }
    }
}
