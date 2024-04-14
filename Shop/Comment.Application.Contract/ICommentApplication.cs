
using _0_Framework.Application;

namespace Comment.Application.Contract
{
    public interface ICommentApplication
    {
        OperationResult Create(AddComment Command);
        public List<CommentViewModel> Search(CommentSearchModel searchModel);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);
    }
}
