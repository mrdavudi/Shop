using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Repository;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _shopContext;

        public ProductRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public EditProduct GetDetails(long id)
        {
            return _shopContext.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Code = x.Code,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription
            }).FirstOrDefault(x=>x.Id == id);
        }

        public List<productViewModel> GetProducts()
        {
            return _shopContext.Products.Select(x => new productViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public Product GetProductWithCategory(long id)
        {
            return _shopContext.Products
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<productViewModel> search(productSearchModel searchmodel)
        {
            var query = _shopContext.Products
                .Include(x => x.Category)
                .Select(x => new productViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                CategoryId = x.CategoryId,
                Code = x.Code,
                Picture = x.Picture,
                CreationDate = x.CreatetionDateTime.ToString()
            });

            if (!string.IsNullOrWhiteSpace(searchmodel.Name))
                query = query.Where(x => x.Name == searchmodel.Name);

            if (searchmodel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchmodel.CategoryId);

            if (!string.IsNullOrWhiteSpace(searchmodel.Code))
                query = query.Where(x => x.Code.Contains(searchmodel.Code));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
