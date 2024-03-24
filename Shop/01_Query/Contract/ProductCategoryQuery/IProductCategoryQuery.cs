using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Contract.ProductCategoryQuery
{
    public interface IProductCategoryQuery
    {
        public List<ProductCategoryQueryModel> GetProductCategories();
    }
}
