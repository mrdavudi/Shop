using _01_Query.Contract.Blog.Article;
using _01_Query.Contract.Blog.ArticleCategory;

namespace _01_Query.Contract.Blog.Article
{
    public interface IArticleQuery
    {
        public List<ArticleQueryModel> GetLatestArticles();
        public ArticleQueryModel GetArticleDetails(string slug);
    }
}
