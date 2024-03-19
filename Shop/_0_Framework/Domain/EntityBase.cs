using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Domain
{
    public class EntityBase
    {
        public int Id { get; private set; }
        public DateTime CreatetionDateTime { get; private set; }

        public EntityBase()
        {
            CreatetionDateTime = DateTime.Now;
        }
    }
}
