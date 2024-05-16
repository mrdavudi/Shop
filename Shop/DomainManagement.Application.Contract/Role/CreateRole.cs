using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using _0_Framework.Repository.Permissions;

namespace DomainManagement.Application.Contract.Role
{
    public class CreateRole
    {
        [Required (ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

    }
}