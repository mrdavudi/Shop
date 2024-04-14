using _01_Query.Contract.Blog.Article;
using BlogManagement.Application.Contract.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHosts.ViewComponent
{
    public class BlogViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IArticleQuery _articleQuery;

        public BlogViewComponent(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public IViewComponentResult Invoke()
        {
            var latestArticles = _articleQuery.GetLatestArticles();
            return View(latestArticles);
        }
    }
}
