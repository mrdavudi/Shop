using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Repository;
using AccountManagement.Domain.AccountAgg;
using DomainManagement.Application.Contract.Account;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _accountContext;
        public AccountRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public Account GetAccountBy(string userName)
        {
            return _accountContext.Account.FirstOrDefault(X => X.UserName == userName);
        }

        public EditAccount GetDetails(long id)
        {
            return _accountContext.Account.Select(x => new EditAccount
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
                Mobile = x.Mobile,
                RoleId = x.RoleId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _accountContext.Account
                .Include(x=>x.Role)
                .Select(x => new AccountViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Mobile = x.Mobile,
                    ProfilePhoto = x.ProfilePhoto,
                    Role = x.Role.Name,
                    RoleId = x.RoleId,
                    UserName = x.UserName,
                    CreationDate = x.CreatetionDateTime.ToFarsi()
                });

            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));

            if (searchModel.RoleId > 0)
                query = query.Where(x => x.RoleId == searchModel.RoleId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
