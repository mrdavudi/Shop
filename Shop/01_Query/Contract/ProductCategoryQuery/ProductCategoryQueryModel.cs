using _01_Query.Contract.Product;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ShopManagement.Application.Contract.Product;

namespace _01_Query.Contract.ProductCategoryQuery
{
    public class ProductCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Description { get; set; }
        public List<ProductQueryModel> Products { get; set; }
    }
}