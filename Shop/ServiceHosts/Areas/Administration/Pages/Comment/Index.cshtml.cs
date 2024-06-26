using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Comment.Application.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHosts.Areas.Administration.Pages.Comment
{
    public class IndexModel : PageModel
    {
        private readonly ICommentApplication _commentApplication;
        public List<CommentViewModel> CommentList;
        public CommentSearchModel SearchModel;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            CommentList = _commentApplication.Search(searchModel);
        }

        public IActionResult OnGetConfirm(long id)
        {
            _commentApplication.Confirm(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            _commentApplication.Cancel(id);
            return RedirectToPage("./Index");
        }
    }
}
