using _0_Framework.Domain;
using Comment.Application.Contract;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<long, Comments>
    {
        public List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
