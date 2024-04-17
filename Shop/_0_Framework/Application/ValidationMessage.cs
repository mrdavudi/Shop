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
        public const string PasswordNotMatch = "رمز عبور با تکرار آن برابر نیست.";
        public const string WrongUserNameOrPassword = "نام کاربری یا رمز عبور نادرست است.";
    }
}
