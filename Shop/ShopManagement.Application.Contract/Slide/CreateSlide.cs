using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contract.Slide
{
    public class CreateSlide
    {
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtensionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessage.ValidExtensions)]
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string BtnText { get; set; }
        public string Link { get; set; }
        public bool IsRemoved { get; set; }
    }
}