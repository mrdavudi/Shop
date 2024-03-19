using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long, Product>
    {
        public Product GetProductWithCategory(long id);
        public EditProduct GetDetails(long id);
        public List<productViewModel> GetProducts();
        public List<productViewModel> search(productSearchModel searchmodel);
    }
}
