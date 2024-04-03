using _0_Framework.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;

        public ProductPictureApplication(IProductRepository productRepository, IProductPictureRepository productPictureRepository, IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
        }  

        public OperationResult Create(CreateProductPicture command)
        {
            var operationResult = new OperationResult();

            var product = _productRepository.GetProductWithCategory(command.ProductId);

            var path = $"{product.Category.Slug}//{product.Slug}";
            var pictureName = _fileUploader.Upload(command.Pictures, path);

            var productPicture = new ProductPicture(pictureName, command.PictureTitle, command.PictureAlt,
                command.ProductId);

            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operationResult = new OperationResult();
            var productPicture = _productPictureRepository.GetProductCategoryWithPicture(command.Id);

            if (productPicture == null | command.ProductId == 0)
                return operationResult.Failed("فیلد های اجباری را تکمیل کنید");

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var path = $"{product.Category.Slug}//{product.Slug}";

            var pictureName = _fileUploader.Upload(command.Pictures, path);
            productPicture.Edit(pictureName, command.PictureTitle, command.PictureAlt, command.ProductId);
            _productPictureRepository.SaveChange();

            return operationResult.Succeded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operationResult.Failed("تصویر مورد نظر یافت نشد");

            productPicture.Remove(id);
            _productPictureRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);

            if (productPicture == null)
                return operationResult.Failed("تصویر مورد نظر یافت نشد");
            
            productPicture.Restore(id);
            _productPictureRepository.SaveChange();

            return operationResult.Succeded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
