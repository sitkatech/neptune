/*-----------------------------------------------------------------------
<copyright file="SitkaFileExtensionsAttribute.cs" company="Sitka Technology Group">
Copyright (c) Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Common.Mvc
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SitkaFileExtensionsAttribute : ValidationAttribute, IClientModelValidator
    {
        public List<string> ValidExtensions { get; set; }

        public SitkaFileExtensionsAttribute(string fileExtensions)
        {
            ValidExtensions = fileExtensions.ToLower().Split('|').ToList();
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var fileName = file.FileName.ToLower();
                if (!ValidExtensions.Any(fileName.EndsWith))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Uploaded file needs to be one of the following extensions: {string.Join(", ", ValidExtensions)}";
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-fileextensions", GetErrorMessage());
            MergeAttribute(context.Attributes, "data-val-fileextensions-fileextensions", string.Join(",", ValidExtensions));
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
