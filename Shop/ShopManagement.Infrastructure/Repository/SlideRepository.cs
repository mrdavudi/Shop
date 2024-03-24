using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Repository;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _shopContext;
        public SlideRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public EditSlide GetDetails(long id)
        {
            return _shopContext.Slides
                .Select(x => new EditSlide
                {
                    Id = x.Id,
                    BtnText = x.BtnText,
                    Heading = x.Heading,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Text = x.Text,
                    Link = x.Link,
                    Title = x.Title
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _shopContext.Slides
                .Select(x => new SlideViewModel
                {
                    Id = x.Id,
                    Heading = x.Heading,
                    Picture = x.Picture,
                    Title = x.Title,
                    IsRemoved = x.IsRemoved,
                    CreationDate = x.CreatetionDateTime.ToString()
                })
                .OrderByDescending(x => x.Id)
                .ToList();
        }
    }
}
