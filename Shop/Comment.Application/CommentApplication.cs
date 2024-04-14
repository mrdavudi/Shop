using _0_Framework.Application;
using Comment.Application.Contract;
using CommentManagement.Infrastructure.Repository;

namespace Comment.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly CommentRepository _commentRepository;

        public CommentApplication(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Cancel(long id)
        {
            var operationResult = new OperationResult();
            var GetComment = _commentRepository.Get(id);

            if (GetComment == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            GetComment.Cancel();
            _commentRepository.SaveChange();
            return operationResult.Succeded();
        }

        public OperationResult Confirm(long id)
        {
            var operationResult = new OperationResult();
            var GetComment = _commentRepository.Get(id);

            if (GetComment == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            GetComment.Confirm();
            _commentRepository.SaveChange();
            return operationResult.Succeded();
        }

        public OperationResult Create(AddComment Command)
        {
            var operationResult = new OperationResult();

            var createComment = new CommentManagement.Domain.CommentAgg.Comment(Command.Name, Command.Email,
                Command.Website, Command.Message, Command.Type, Command.OwnerRecordId, Command.ParentId);

            _commentRepository.Create(createComment);
            _commentRepository.SaveChange();
            return operationResult.Succeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
