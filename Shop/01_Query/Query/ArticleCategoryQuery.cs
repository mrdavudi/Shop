using _0_Framework.Application;
using _01_Query.Contract.Blog.Article;
using _01_Query.Contract.Blog.ArticleCategory;
using Blog.Management.Domain.ArticleAgg;
using BlogManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _blogContext.ArticleCategories
                .Include(x=>x.Articles)
                .Select(x => new ArticleCategoryQueryModel
            {
                Name = x.Name,
                ArticlesCount = x.Articles.Count,
                Slug = x.Slug,
            }).ToList();
        }

        public ArticleCategoryQueryModel GetArticleListInCategory(string slug)
        {
            var articleCategories =  _blogContext.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Slug = x.Slug,
                    Name = x.Name,
                    ArticlesCount = x.Articles.Count,
                    Keywords = x.Keywords,
                    Articles = MapArticles(x.Articles)
                    
                }).FirstOrDefault(x => x.Slug == slug);

            if(!string.IsNullOrWhiteSpace(articleCategories.Keywords))
                articleCategories.KeywordList = articleCategories.Keywords.Split(",").ToList();

            return articleCategories;
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel
            {
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Title = x.Title,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,
            }).ToList();
        }
    }
}
