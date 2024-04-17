using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using DomainManagement.Application.Contract.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IRepository<long, Role>
    {
        List<RoleViewModel> List();
        EditRole GetDetails(long id);
    }
}
