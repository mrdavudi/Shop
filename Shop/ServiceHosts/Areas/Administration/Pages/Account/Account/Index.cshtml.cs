using DomainManagement.Application.Contract.Account;
using DomainManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHosts.Areas.Administration.Pages.Account.Account
{
    [Authorize (Roles = "1")]
    public class IndexModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        public AccountSearchModel SearchModel;
        public List<AccountViewModel> AccountList;
        public SelectList RolesList;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            AccountList = _accountApplication.Search(searchModel);
            RolesList = new SelectList(_roleApplication.RoleList(), "Id", "Name");
        }

        public IActionResult OnGetRegister()
        {
            var command = new RegisterAccount
            {
                Roles = _roleApplication.RoleList()
            };
            return Partial("./Register", command);
        }

        public JsonResult OnPostCreate(RegisterAccount command)
        {
            var registerUser = _accountApplication.Register(command);
            return new JsonResult(registerUser);
        }

        public IActionResult OnGetEdit(long id)
        {
            var accountEdit = _accountApplication.GetDetails(id);
            accountEdit.Roles = _roleApplication.RoleList();
            return Partial("./Edit", accountEdit);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            var EditUser = _accountApplication.Edit(command);
            return new JsonResult(EditUser);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChangePassword { Id = id };
            return Partial("./ChangePassword", command);
        }

        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            var ChangePassword = _accountApplication.ChangePassword(command);
            return new JsonResult(ChangePassword);
        }
    }
}
