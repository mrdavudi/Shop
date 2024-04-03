
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Application.Contract.ProductPicture
{
    public class CreateProductPicture
    {
        public long ProductId { get; set; }
        
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtensionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessage.ValidExtensions)]
        public IFormFile Pictures { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public List<productViewModel> Products { get; set; }

   
    }
}