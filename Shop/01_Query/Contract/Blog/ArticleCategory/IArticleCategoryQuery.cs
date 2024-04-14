using _01_Query.Contract.Blog.Article;
using _01_Query.Contract.Blog.ArticleCategory;

namespace _01_Query.Contract.Blog.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        public List<ArticleCategoryQueryModel> GetArticleCategories();
        public ArticleCategoryQueryModel GetArticleListInCategory(string slug);
    }
}
