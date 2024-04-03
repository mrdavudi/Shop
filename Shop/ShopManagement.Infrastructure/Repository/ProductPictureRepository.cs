using _0_Framework.Application;
using _0_Framework.Repository;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _shopContext;
        public ProductPictureRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _shopContext.ProductPictures
                .Select(x => new EditProductPicture
                {
                    Id = x.Id,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ProductId = x.ProductId
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public ProductPicture GetProductCategoryWithPicture(long id)
        {
            return _shopContext.ProductPictures
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _shopContext.ProductPictures
                .Select(x => new ProductPictureViewModel
                {
                    Id = x.Id,
                    Product = x.Products.Name,
                    CreationDate = x.CreatetionDateTime.ToFarsi(),
                    Picture = x.Picture,
                    ProductId = x.ProductId,
                    IsRemoved = x.IsRemoved
                });

            if (searchModel.ProductId != 0)
                query = query.Where(x=>x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
