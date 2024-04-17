using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Repository;
using AccountManagement.Domain.RoleAgg;
using DomainManagement.Application.Contract.Role;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _accountContext;
        public RoleRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public EditRole GetDetails(long id)
        {
            return _accountContext.Role
                .Select(x => new EditRole
                {
                    Id = x.Id,
                    Name = x.Name
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<RoleViewModel> List()
        {
            return _accountContext.Role
                .Select(x => new RoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreatetionDateTime.ToFarsi()
                }).AsNoTracking().OrderByDescending(x=>x.Id).ToList();
        }
    }
}
