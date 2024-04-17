using _0_Framework.Application;

namespace DomainManagement.Application.Contract.Account
{
    public interface IAccountApplication
    {
        public OperationResult Register(RegisterAccount command);
        public OperationResult Login(LoginAccount command);
        public void Logout();
        public OperationResult Edit(EditAccount command);
        public List<AccountViewModel> Search(AccountSearchModel searchModel);
        public EditAccount GetDetails(long id);
        public OperationResult ChangePassword(ChangePassword command);
    }
}
