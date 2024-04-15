using System.Security.Claims;
using _0_Framework.Application;
using _01_Query.Contract.Blog.Article;
using _01_Query.Contract.Comments;
using BlogManagement.Infrastructure;
using Comment.Application.Contract;
using CommentManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentsContext _commentsContext;

        public ArticleQuery(BlogContext blogContext, CommentsContext commentsContext)
        {
            _blogContext = blogContext;
            _commentsContext = commentsContext;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _blogContext.Articles
                .Include(x => x.ArticleCategories)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryName = x.ArticleCategories.Name,
                    CategorySlug = x.ArticleCategories.Slug,
                    Slug = x.Slug,
                    CanonicalAddress = x.CanonicalAddress,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription,
                }).FirstOrDefault(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords.Split(",").ToList();

            var comments = article.CommentList = _commentsContext.Comments
                .Where(x=> !x.IsCanceled)
                .Where(x=> x.IsConfirmed)
                .Where(x=>x.Type == CommentType.ArticleType)
                .Where(x=>x.OwnerRecordId == article.Id)
                .Select(x => new CommentsQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    CreationDate = x.CreatetionDateTime.ToFarsi(),
                    ParentId = x.ParentId,
                }).ToList();

            foreach (var comment in comments)
            {
                if(comment.ParentId > 0)
                    comment.parentName = comments.FirstOrDefault(x => x.Id == comment.ParentId).Name;
            }

            article.CommentList = comments;
            return article;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _blogContext.Articles.Select(x => new ArticleQueryModel
            {
                Title = x.Title,
                Slug = x.Slug,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription,
                CategoryId = x.CategoryId,
            }).ToList();

        }
    }
}
