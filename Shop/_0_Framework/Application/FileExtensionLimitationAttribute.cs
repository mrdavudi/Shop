using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application
{
    public class FileExtensionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _fileExtension;

        public FileExtensionLimitationAttribute(string[] fileExtension)
        {
            _fileExtension = fileExtension;
        }


        public override bool IsValid(object? value)
        {
            var file = value as IFormFile;
            if (file == null) return true;

            var fileExtension = Path.GetExtension(file.FileName);
            return _fileExtension.Contains(fileExtension);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            //context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-fileExtensionLimitation", "پسوند فایل نادرست است.");
        }
    }
}
