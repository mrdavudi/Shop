using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Contract.Product
{
    public interface IProductQuery
    {
        public List<ProductQueryModel> GeLatestArrivals();
        public List<ProductQueryModel> Search(string value);
        public ProductQueryModel GetProductDetail(string slug);
    }
}
