using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var OperationResult = new OperationResult();

            if (_productRepository.Exist(command.Name))
                return OperationResult.Failed("نام مورد نظر تکراری است");

            var slug = command.Slug.Slugify();
            var product = new Product(command.Name, command.ShortDescription, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, slug, command.Keywords, command.MetaDescription);
            _productRepository.CreateProduct(product);
            _productRepository.SaveChange();

            return OperationResult.IsSucceded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var OperationResult = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);

            if (product == null)
                return OperationResult.Failed("رکوردی یافت نشد");

            if (_productRepository.Exist(command.Name))
                return OperationResult.Failed("نام مورد نظر تکراری است");

            var slug = command.Slug.Slugify();
            product.Edit(command.Name, command.ShortDescription, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, slug, command.Keywords, command.MetaDescription);
            _productRepository.SaveChange();

            return OperationResult.IsSucceded();

        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<productViewModel> GetProduct()
        {
            return _productRepository.GetProducts();
        }

        public List<productViewModel> Search(productSearchModel searchModel)
        {
            return _productRepository.search(searchModel);
        }
    }
}
