using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace _0_Framework.Application
{
    public class ValidationMessage
    {
        public const string IsRequired = "این فیلد ضروری است.";
        public const string MaxFileSize = "حجم فایل بیش از حد مجاز است.";
        public const string ValidExtensions = "پسوند فایل نادرست است.";
        public const string DuplicatedRecord = "رکورد تکراری است.";
        public const string RecordNotFound = "رکورد مورد نظر یافت نشد.";
    }
}
