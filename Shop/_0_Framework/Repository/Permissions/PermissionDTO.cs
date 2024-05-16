using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Repository.Permissions
{
    public class PermissionDTO
    {
        public string Name { get; set; }
        public int Code { get; set; }

        public PermissionDTO(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
