namespace _01_Query.Contract.ProductCategoryQuery
{
    public interface IProductCategoryQuery
    {
        public List<ProductCategoryQueryModel> GetProductCategories();
        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
        public ProductCategoryQueryModel GetProductCategoriesWithProductBy(string CategorySlug);
    }
}
