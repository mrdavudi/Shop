using _0_Framework.Domain;
using DomainManagement.Application.Contract.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        public List<AccountViewModel> Search(AccountSearchModel searchModel);
        public EditAccount GetDetails(long id);
        public Account GetAccountBy(string userName);
    }
}
