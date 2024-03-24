using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture : EntityBase
    {
        public string Picture { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public bool IsRemoved { get; private set; }
        public long ProductId { get; private set; }
        public Product Products { get; private set; }

        public ProductPicture (string picture, string pictureTitle, string pictureAlt, long productId)
        {
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            IsRemoved = false;
            ProductId = productId;
        }

        public void Edit(string picture, string pictureTitle, string pictureAlt, long productId)
        {
            if(!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            ProductId = productId;
        }

        public void Remove(long id)
        {
            IsRemoved = true;
        }

        public void Restore(long id)
        {
            IsRemoved = false;
        }
    }
}
