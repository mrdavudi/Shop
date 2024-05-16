using _0_Framework.Application;
using _0_Framework.Application.Auth;
using _0_Framework.Application.PasswordHash;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using DomainManagement.Application.Contract.Account;

namespace DomainManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication()
        {

        }

        public AccountApplication(IAccountRepository accountRepository, IFileUploader fileUploader
            , IAuthHelper authHelper, IPasswordHasher passwordHasher, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operationResult = new OperationResult();
            if (_accountRepository.Exist(x => x.UserName == command.UserName))
                return operationResult.Failed(ValidationMessage.DuplicatedRecord);

            var password = _passwordHasher.Hash(command.Password);
            var pictureName = _fileUploader.Upload(command.ProfilePhoto, "ProfilePicture");

            var newAccount = new Account(command.FullName, command.UserName, password, command.Mobile,
                command.RoleId, pictureName);
            _accountRepository.Create(newAccount);
            _accountRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Login(LoginAccount command)
        {
            var operationResult = new OperationResult();

            var accountInfo = _accountRepository.GetAccountBy(command.UserName);

            if (accountInfo == null)
                return operationResult.Failed(ValidationMessage.WrongUserNameOrPassword);

            var password = _passwordHasher.Check(accountInfo.Password, command.Password);

            if (!password.Verified)
                return operationResult.Failed(ValidationMessage.WrongUserNameOrPassword);

            var permissions = _roleRepository.Get(accountInfo.RoleId).Permissions.Select(x => x.Code).ToList();

            var authViewModel = new AuthViewModel(accountInfo.Id, accountInfo.RoleId, accountInfo.FullName,
                accountInfo.UserName, accountInfo.Mobile, permissions);

            _authHelper.SignIn(authViewModel);

            return operationResult.Succeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operationResult = new OperationResult();

            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_accountRepository.Exist(x => x.UserName == command.UserName && x.Id != command.Id))
                return operationResult.Failed(ValidationMessage.DuplicatedRecord);

            var PictureName = _fileUploader.Upload(command.ProfilePhoto, "ProfilePicture");
            account.Edit(command.FullName, command.UserName, command.Mobile, command.RoleId, PictureName);
            _accountRepository.SaveChange();

            return operationResult.Succeded();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operationResult = new OperationResult();

            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (command.Password != command.RePassword)
                return operationResult.Failed(ValidationMessage.PasswordNotMatch);

            account.ChangePassword(_passwordHasher.Hash(command.Password));
            _accountRepository.SaveChange();

            return operationResult.Succeded();
        }
    }
}
