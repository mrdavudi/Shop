﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace _01_Query.Contract.ProductCategoryQuery
{
    public class ProductCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
    }
}