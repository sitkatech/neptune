/*-----------------------------------------------------------------------
<copyright file="SitkaModelBinder.cs" company="Sitka Technology Group">
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

using Microsoft.AspNetCore.Mvc.ModelBinding;
using IModelBinder = Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder;
using ModelBindingContext = Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext;
using ValueProviderResult = Microsoft.AspNetCore.Mvc.ModelBinding.ValueProviderResult;

namespace Neptune.Web.Common.Models
{
    /// <summary>
    /// Provides easy way to make <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder"/>
    /// </summary>
    public abstract class SitkaModelBinder : IModelBinder
    {
        private readonly Func<string, object> _stringConstructorFunc;

        protected SitkaModelBinder(Func<string, object> stringConstructorFunc)
        { 
            _stringConstructorFunc = stringConstructorFunc;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            try
            {
                var model = ConstructFromString(value);
                bindingContext.Result = ModelBindingResult.Success(model);
                return Task.CompletedTask;
            }
            catch (FormatException e)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, e.Message);
                return Task.CompletedTask;
            }
        }

        private object ConstructFromString(string stringValue)
        {
            return ((Func<string, object>)(s => _stringConstructorFunc(s)))(stringValue);
        }
    }
}
