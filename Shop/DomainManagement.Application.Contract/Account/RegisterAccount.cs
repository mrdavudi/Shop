using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Xml;
using _0_Framework.Application;
using DomainManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.CompilerServices;

namespace DomainManagement.Application.Contract.Account
{
    public class RegisterAccount
    {
        [Required (ErrorMessage = ValidationMessage.IsRequired)]
        public string FullName { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Mobile { get; set; }

        [Range (1, long.MaxValue ,ErrorMessage = ValidationMessage.IsRequired)]
        public long RoleId { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtensionLimitation(new string[]{".jpg", ".png", ".jpeg"}, ErrorMessage = ValidationMessage.ValidExtensions)]
        public IFormFile ProfilePhoto { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}