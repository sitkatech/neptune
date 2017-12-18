/*-----------------------------------------------------------------------
<copyright file="NeptuneBaseController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;
using log4net;
using SitkaController = Neptune.Web.Common.SitkaController;

namespace Neptune.Web.Controllers
{
    [ValidateInput(false)]
    public abstract class NeptuneBaseController : SitkaController
    {
        public static ControllerContext ControllerContextStatic = null;

        protected ILog Logger = LogManager.GetLogger(typeof(NeptuneBaseController));

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!IsCurrentUserAnonymous())
            {
                CurrentPerson.LastActivityDate = DateTime.Now;
                HttpRequestStorage.DatabaseEntities.ChangeTracker.DetectChanges();
                HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
            }
            base.OnAuthorization(filterContext);
        }

        protected NeptuneBaseController()
        {
            if (ControllerContextStatic == null)
                ControllerContextStatic = ControllerContext;
        }

        public static ReadOnlyCollection<MethodInfo> AllControllerActionMethods => AllControllerActionMethodsProtected;

        static NeptuneBaseController()
        {
            AllControllerActionMethodsProtected = new ReadOnlyCollection<MethodInfo>(GetAllControllerActionMethods(typeof(NeptuneBaseController)));
        }

        protected override bool IsCurrentUserAnonymous()
        {
            return CurrentPerson == null || CurrentPerson.IsAnonymousUser;
        }

        protected override string LoginUrl => NeptuneHelpers.GenerateLogInUrlWithReturnUrl();

        protected override ISitkaDbContext SitkaDbContext => HttpRequestStorage.DatabaseEntities;

        protected Person CurrentPerson => HttpRequestStorage.Person;

        protected Tenant CurrentTenant => HttpRequestStorage.Tenant;
    }
}
