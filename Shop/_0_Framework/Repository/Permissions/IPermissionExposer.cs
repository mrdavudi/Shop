using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Repository.Permissions
{
    public interface IPermissionExposer
    {
        public Dictionary<string, List<PermissionDTO>> Expose();
    }
}
