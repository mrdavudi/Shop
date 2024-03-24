using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }  

        public OperationResult Create(CreateProductPicture command)
        {
            var operationResult = new OperationResult();

            var product = _productPictureRepository.GetProductCategoryWithPicture(command.ProductId);

            var productPicture = new ProductPicture(command.Pictures, command.PictureTitle, command.PictureAlt,
                command.ProductId);

            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operationResult = new OperationResult();
            var product = _productPictureRepository.GetProductCategoryWithPicture(command.Id);

            if (product == null | command.ProductId == 0)
                return operationResult.Failed("فیلد های اجباری را تکمیل کنید");

            product.Edit(command.Pictures, command.PictureTitle, command.PictureAlt, command.ProductId);
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
