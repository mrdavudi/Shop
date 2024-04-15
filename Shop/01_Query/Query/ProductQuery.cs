using _0_Framework.Application;
using _01_Query.Contract.Comments;
using _01_Query.Contract.Product;
using CommentManagement.Infrastructure;
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
        private readonly CommentsContext _commentsContext;

        public ProductQuery(ShopContext shopContext, DiscountContext discountContext, InventoryContext inventoryContext, CommentsContext commentsContext)
        {
            _shopContext = shopContext;
            _discountContext = discountContext;
            _inventoryContext = inventoryContext;
            _commentsContext = commentsContext;
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

        public ProductQueryModel GetProductDetail(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice, x.InStock }).AsNoTracking().ToList();
            var Discount = _discountContext.CustomerDiscount
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).AsNoTracking().ToList();
            
            var ProductDetail = _shopContext.Products
                .Include(x => x.Category)
                .ThenInclude(x => x.Products)
                .Select(x=> new ProductQueryModel
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Name = x.Name,
                    Category = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    Code = x.Code,
                    ShortDescription = x.ShortDescription,
                    Slug = x.Slug,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription
                })
                .AsNoTracking()
                .FirstOrDefault(x => x.Slug == slug);

            if (ProductDetail != null)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == ProductDetail.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    ProductDetail.Price = price.ToMoney();
                    ProductDetail.IsInStock = productInventory.InStock;

                    var productDiscount = Discount.FirstOrDefault(x => x.ProductId == ProductDetail.Id);
                    if (productDiscount != null)
                    {
                        var discountRate = productDiscount.DiscountRate;
                        var discountPrice = Math.Round(price * discountRate / 100);

                        ProductDetail.DiscountRate = productDiscount.DiscountRate;
                        ProductDetail.PriceWithDiscount = (price - discountPrice).ToMoney();
                        ProductDetail.HasDiscount = discountRate > 0;
                        ProductDetail.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();
                    }
                }

                ProductDetail.CommentsList = _commentsContext.Comments
                    .Where(x => x.IsConfirmed)
                    .Where(x => !x.IsCanceled)
                    .Where(x => x.Type == CommentType.ProductType)
                    .Where(x => x.OwnerRecordId == ProductDetail.Id)
                    .Select(x => new CommentsQueryModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Message = x.Message,
                        CreationDate = x.CreatetionDateTime.ToFarsi(),
                        ParentId = x.ParentId
                    })
                    .AsNoTracking().OrderByDescending(x=>x.Id).ToList();

            }

            return ProductDetail;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discount = _discountContext.CustomerDiscount
                .Where(x=> x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var searchProduct = _shopContext.Products
                .Include(x=>x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Category = product.Category.Name,
                    CategorySlug = product.Category.Slug,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    ShortDescription = product.ShortDescription,
                    Slug = product.Slug
                }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
            {
                searchProduct = searchProduct.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));
            }

            var products = searchProduct.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                        var DiscountRate = productDiscount.DiscountRate;
                        var discountPrice = Math.Round(price * DiscountRate) / 100;
                        product.PriceWithDiscount = (price - discountPrice).ToMoney();
                        product.HasDiscount = DiscountRate > 0;
                        product.DiscountRate = DiscountRate;
                        product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();
                    }
                }
            }

            return products;
        }
    }
}
