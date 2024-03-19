using _0_Framework.Application;

namespace ShopManagement.Application.Contract.ProductCategory
{
    public interface IProductCategoryApplication
    {
        public OperationResult Create(CreateProductCategory command);
        public OperationResult Edit(EditProductCategory command);
        public EditProductCategory GetDetail(long id);
        public List<ProductCategoryViewModel> GetProductCategories();
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchmodel);
    }
}
