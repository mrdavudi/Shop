using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Contract.Comments
{
    public class CommentsQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string CreationDate { get; set; }
        public long ParentId { get; set; }
        public string parentName { get; set; }
    }
}
