using _0_Framework.Repository.Permissions;

namespace DomainManagement.Application.Contract.Role
{
    public class EditRole : CreateRole
    {
        public long Id { get; set; }
        public List<PermissionDTO> MappedPermissions { get; set; }
        public List<int> Permissions { get; set; }
    }
}