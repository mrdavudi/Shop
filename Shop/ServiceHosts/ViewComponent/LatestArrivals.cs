using _01_Query.Contract.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHosts.ViewComponent
{
    public class LatestArrivals : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public LatestArrivals(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var latestArrivals = _productQuery.GeLatestArrivals();
            return View(latestArrivals);
        }
    }
}
