using _01_Query.Contract.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHosts.ViewComponent
{
    public class SlideViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public ISlideQuery _SlideQuery;

        public SlideViewComponent(ISlideQuery slideQuery)
        {
            _SlideQuery = slideQuery;
        }
        public IViewComponentResult Invoke()
        {
            var SlideList = _SlideQuery.GetSlide();
            return View(SlideList);
        }
    }
}
