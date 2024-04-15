using _01_Query.Contract.Blog.Article;
using _01_Query.Contract.Blog.ArticleCategory;
using _01_Query.Contract.Comments;
using Comment.Application.Contract;
using CommentManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class ArticleDetailsModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;
        public ArticleQueryModel articleDetails;
        public List<ArticleCategoryQueryModel> CategoryList;
        public List<ArticleQueryModel> articleList;

        public ArticleDetailsModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryApplication, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryApplication;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            articleDetails = _articleQuery.GetArticleDetails(id);
            CategoryList = _articleCategoryQuery.GetArticleCategories();
            articleList = _articleQuery.GetLatestArticles();
        }

        public IActionResult OnPost(AddComment command, string ArticleSlug)
        {
            command.Type = CommentType.ArticleType;
            var CreateComment = _commentApplication.Create(command);

            return RedirectToPage("/ArticleDetails", new { Id = ArticleSlug });
        }
    }
}
