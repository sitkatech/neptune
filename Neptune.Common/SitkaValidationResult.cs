﻿/*-----------------------------------------------------------------------
<copyright file="SitkaValidationResult.cs" company="Sitka Technology Group">
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

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Neptune.Common
{
    public class SitkaValidationResult<TObject, TProperty> : ValidationResult
    {
        public SitkaValidationResult(string errorMessage, Expression<Func<TObject, TProperty>> propertyNameLambda) : base(errorMessage, GetPropertyNameArray(propertyNameLambda)) {}

        private static IEnumerable<string> GetPropertyNameArray(Expression<Func<TObject, TProperty>> propertyLambda)
        {
            var type = typeof(TObject);
            if (propertyLambda.Body is not MemberExpression member)
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");
            }
            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
            }
            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a property that is not from type {type}.");
            }

            return new[] {propInfo.Name};
        }
    }
}
