using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Query.Contract.Slide;
using ShopManagement.Infrastructure;

namespace _01_Query.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<SlideQueryModel> GetSlide()
        {
            return _shopContext.Slides
                .Where(x=>x.IsRemoved == false)
                .Select(x=>new SlideQueryModel
                {
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    BtnText = x.BtnText,
                    Heading = x.Heading,
                    Link = x.Link,
                    Text = x.Text,
                    Title = x.Title
                })
                .ToList();
        }
    }
}
