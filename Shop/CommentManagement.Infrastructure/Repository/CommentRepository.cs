using _0_Framework.Application;
using _0_Framework.Repository;
using Comment.Application.Contract;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.Repository
{
    public class CommentRepository : RepositoryBase<long, Domain.CommentAgg.Comment>, ICommentRepository
    {
        private readonly CommentContext _commentContext;
        public CommentRepository(CommentContext commentContext) : base(commentContext)
        {
            _commentContext = commentContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _commentContext.Comments
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Website = x.Website,
                    Message = x.Message,
                    OwnerRecordId = x.OwnerRecordId,
                    Type = x.Type,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    CommentDate = x.CreatetionDateTime.ToFarsi()
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
