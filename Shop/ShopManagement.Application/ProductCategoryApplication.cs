using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operationResult = new OperationResult();

            if (_productCategoryRepository.Exist(x=> x.Name == command.Name))
                return operationResult.Failed("نام مورد نظر تکراری است.");

            var slug = command.Slug.Slugify();

            var path = command.Slug;
            var Picture = _fileUploader.Upload(command.Picture, path);

            var productCategory = new ProductCategory(command.Name, command.Description, Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var productCategory = _productCategoryRepository.Get(command.Id);
            var operationResult = new OperationResult();

            if (productCategory == null)
                return operationResult.Failed();

            if (_productCategoryRepository.Exist(x=> x.Name == command.Name && x.Id != command.Id))
                return operationResult.Failed("نام مورد نظر تکراری است.");

            var slug = command.Slug.Slugify();

            var path = command.Slug;
            var Picture = _fileUploader.Upload(command.Picture, path);

            productCategory.Edit(command.Name, command.Description, Picture, command.PictureAlt,
                command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.SaveChange();

            return operationResult.Succeded();
        }

        public EditProductCategory GetDetail(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategory();
        }

        public string GetProductCategorySlugBy(long id)
        {
            return _productCategoryRepository.GetProductCategorySlugBy(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchmodel)
        {
            return _productCategoryRepository.Search(searchmodel);
        }
    }
}
