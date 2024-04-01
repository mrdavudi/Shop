using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_Query.Contract.Product;
using _01_Query.Contract.ProductCategoryQuery;
using DiscountManagement.Infrastructure;
using InventoryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure;

namespace _01_Query.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly DiscountContext _discountContext;
        private readonly InventoryContext _inventoryContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopContext.ProductCategories
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Slug = x.Slug
                })
                .ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discount = _discountContext.CustomerDiscount
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();

            var Categories = _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).ToList();

            foreach (var category in Categories)
            {
                foreach (var product in category.Products)
                {
                    var productInInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInInventory != null)
                    {
                        var price = productInInventory.UnitPrice;
                        product.Price = price.ToMoney();
                        var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);

                        if (productDiscount != null)
                        {
                            var discountRate = productDiscount.DiscountRate;
                            product.HasDiscount = discountRate > 0;
                            product.DiscountRate = discountRate;
                            var discountPrice = Math.Round(price * discountRate / 100);
                            product.PriceWithDiscount = (price - discountPrice).ToMoney();
                        }
                    }
                }
            }

            return Categories;
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Picture = product.Picture,
                PictureTitle = product.PictureTitle,
                PictureAlt = product.PictureAlt,
                Category = product.Category.Name,
                Slug = product.Slug
            }).ToList();
        }
    }
}
