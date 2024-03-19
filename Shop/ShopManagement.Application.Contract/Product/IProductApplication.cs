using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.Product
{
    public interface IProductApplication
    {
        public OperationResult Create(CreateProduct command);
        public OperationResult Edit(EditProduct command);
        public EditProduct GetDetails(long id);
        public List<productViewModel> GetProduct();
        public List<productViewModel> Search(productSearchModel searchModel);

    }
}
