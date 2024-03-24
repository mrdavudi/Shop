
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Application.Contract.ProductPicture
{
    public class CreateProductPicture
    {
        public long ProductId { get; set; }
        public string Pictures { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public List<productViewModel> Products { get; set; }

   
    }
}