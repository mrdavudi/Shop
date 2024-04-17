namespace _0_Framework.Application.Auth
{
    public interface IAuthHelper
    {
        public void SignIn(AuthViewModel command);
        public void SignOut();
        public bool IsAuthenticated();
        public string CurrentAccountRole();
        public AuthViewModel CurrentAccountInfo();
        public long CurrentAccountId();
        public string CurrentAccountMobile();
    }
}
