using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
    {
        public EditProductCategory GetDetails(long id);
        public List<ProductCategoryViewModel> GetProductCategory();
        public string GetProductCategorySlugBy(long id);
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

    }
}
