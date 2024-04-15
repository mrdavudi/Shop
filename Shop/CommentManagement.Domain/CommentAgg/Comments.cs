using _0_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comments : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Website { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public int Type { get; private set; }
        public long OwnerRecordId { get; private set; }
        public long ParentId { get; private set; }
        public Comments Parent { get; private set; }

        public Comments(string name, string email, string website, string message, int type, long ownerRecordId, long parentId)
        {
            Name = name;
            Email = email;
            Website = website;
            Message = message;
            Type = type;
            OwnerRecordId = ownerRecordId;
            ParentId = parentId;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}
