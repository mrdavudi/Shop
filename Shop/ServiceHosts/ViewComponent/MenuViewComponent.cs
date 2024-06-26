﻿using _01_Query.Contract.ProductCategoryQuery;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHosts.ViewComponent
{
    public class MenuViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productCategories = _productCategoryQuery.GetProductCategories();
            return View(productCategories);
        }
    }
}
