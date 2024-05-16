using System.Dynamic;
using DomainManagement.Application.Contract.Account;
using DomainManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHosts.Areas.Administration.Pages.Account.Role
{
    public class IndexModel : PageModel
    {
        private IRoleApplication _roleApplication;
        public List<RoleViewModel> Roles;

        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            Roles = _roleApplication.RoleList();
        }
    }
}
