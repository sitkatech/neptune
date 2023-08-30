/*-----------------------------------------------------------------------
<copyright file="NeptuneFeatureWithContextImpl.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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

using Microsoft.AspNetCore.Mvc.Filters;
using Neptune.Common;
using Neptune.Common.DesignByContract;
using Neptune.EFModels;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;

namespace Neptune.Web.Security
{
    public class NeptuneFeatureWithContextImpl<T> : IActionFilter where T : class, IHavePrimaryKey
    {
        private readonly INeptuneBaseFeatureWithContext<T> _neptuneFeatureWithContext;

        public NeptuneFeatureWithContextImpl(INeptuneBaseFeatureWithContext<T> neptuneFeatureWithContext)
        {
            _neptuneFeatureWithContext = neptuneFeatureWithContext;
        }

        public PermissionCheckResult HasPermission(Person person, T contextModelObject)
        {
            return _neptuneFeatureWithContext.HasPermission(person, contextModelObject);
        }

        public void DemandPermission(Person person, T contextModelObject)
        {
            var permissionCheckResult = HasPermission(person, contextModelObject);
            if (!permissionCheckResult.HasPermission)
            {
                throw new SitkaRecordNotAuthorizedException(permissionCheckResult.PermissionDeniedMessage);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var primaryKeyForObject = GetPrimaryKeyForObject(filterContext);
            var dbContextService = filterContext.HttpContext.RequestServices.GetService(typeof(NeptuneDbContext));
            if (dbContextService == null || !(dbContextService is NeptuneDbContext dbContext))
            {
                throw new ApplicationException(
                    "Could not find injected DbRepository. Can't check rights appropriately!");
            }

            var person = UserContext.GetUserFromHttpContext(dbContext, filterContext.HttpContext);
            DemandPermission(person, primaryKeyForObject.EntityObject);
        }

        protected EntityPrimaryKey<T> GetPrimaryKeyForObject(ActionExecutingContext filterContext)
        {
            var ltInfoEntityPrimaryKeys = filterContext.ActionArguments.Values.OfType<EntityPrimaryKey<T>>().ToList();

            var genericMessage = string.Format(
                "Problem evaluating feature \"{3}\" for controller action \"{0}.{1}()\" while looking for parameter of type \"{2}\".",
                filterContext.Controller.GetType().Name,
                filterContext.ActionDescriptor.DisplayName,
                typeof(EntityPrimaryKey<T>),
                _neptuneFeatureWithContext.FeatureName);

            Check.Require(ltInfoEntityPrimaryKeys.Any(), genericMessage + " Change code to add that parameter.");
            Check.Require(ltInfoEntityPrimaryKeys.Count == 1,
                genericMessage + " Change code so that there's only one of those parameters.");

            var primaryKeyForObject = ltInfoEntityPrimaryKeys.Single();
            return primaryKeyForObject;
        }

        //protected static void SetInfoMessage(ActionExecutingContext filterContext, PermissionCheckResult permissionCheckResult)
        //{
        //    filterContext.Controller.TempData[SitkaController.InfoMessageIndex] = permissionCheckResult.PermissionDeniedMessage;
        //}
    }
}
