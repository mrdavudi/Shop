using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Model.Structures;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Application.Contract.Slide;

namespace ServiceHosts.Areas.Administration.Pages.Shop.Slide
{
    public class IndexModel : PageModel
    {
        private readonly ISlideApplication _slideApplication;
        public List<SlideViewModel> Slides { get; set; }
        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet()
        {
            Slides = _slideApplication.GetList();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateSlide command)
        {
            var modelStateError = ModelState
                .Select(x => x.Value.Errors)
                .Where(x => x.Count > 0).Take(1);

            var slideCreate = new OperationResult().Failed(modelStateError.ToString());
            if (ModelState.IsValid)
            {
                slideCreate = _slideApplication.Create(command);
            }

            return new JsonResult(slideCreate);
        }

        public IActionResult OnGetEdit(long id)
        {
            var editSlide = _slideApplication.GetDetails(id);
            return Partial("./Edit", editSlide);
        }

        public JsonResult OnPostEdit(EditSlide command)
        {
            var modelStateError = ModelState
                .Select(x => x.Value.Errors)
                .Where(x => x.Count > 0).Take(1);

            var slideEdit = new OperationResult().Failed(modelStateError.ToString());
            if (ModelState.IsValid)
            {
                slideEdit = _slideApplication.Edit(command);
            }
            return new JsonResult(slideEdit);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _slideApplication.Remove(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
