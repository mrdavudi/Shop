using _01_Query.Contract.Blog.Article;
using _01_Query.Contract.Blog.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class ArticleDetailsModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        public ArticleQueryModel articleDetails;
        public List<ArticleCategoryQueryModel> CategoryList;
        public List<ArticleQueryModel> articleList;

        public ArticleDetailsModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryApplication)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryApplication;
        }

        public void OnGet(string id)
        {
            articleDetails = _articleQuery.GetArticleDetails(id);
            CategoryList = _articleCategoryQuery.GetArticleCategories();
            articleList = _articleQuery.GetLatestArticles();
        }
    }
}
