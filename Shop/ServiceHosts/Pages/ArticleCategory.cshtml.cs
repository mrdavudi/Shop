using _01_Query.Contract.Blog.Article;
using _01_Query.Contract.Blog.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        public ArticleCategoryQueryModel articleListInCategory;
        public List<ArticleCategoryQueryModel> categoryList;
        public List<ArticleQueryModel> latestAricle;

        public ArticleCategoryModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public void OnGet(string id)
        {
            articleListInCategory = _articleCategoryQuery.GetArticleListInCategory(id);
            categoryList = _articleCategoryQuery.GetArticleCategories();
            latestAricle = _articleQuery.GetLatestArticles();
        }
    }
}
