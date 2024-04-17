using _0_Framework.Application;
using _0_Framework.Application.Auth;
using DomainManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class LoginRegModel : PageModel
    {
        [TempData]
        public string LoginMessage { get; set; }

        [TempData]
        public string RegisterMessage { get; set; }
       
        private readonly IAccountApplication _accountApplication;

        public LoginRegModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(LoginAccount command)
        {
            var login = _accountApplication.Login(command);

            if (login.IsSuccedded)
                return RedirectToPage("/Index");

            LoginMessage = login.Message;
            return RedirectToPage("/LoginReg");
        }

        public IActionResult OnPostRegister(RegisterAccount command)
        {
            var register = _accountApplication.Register(command);
            if (register.IsSuccedded)
                return RedirectToPage("/LoginReg");

            RegisterMessage = register.Message;
            return RedirectToPage("/LoginReg");
        }

        public IActionResult OnGetLogOut()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }

    }
}
