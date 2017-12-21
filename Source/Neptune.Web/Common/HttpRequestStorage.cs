﻿/*-----------------------------------------------------------------------
<copyright file="HttpRequestStorage.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Web;
using LtInfo.Common;
using Neptune.Web.Models;
using Keystone.Common;
using Keystone.Common.OpenID;
using LtInfo.Common.DesignByContract;
using Person = Neptune.Web.Models.Person;

namespace Neptune.Web.Common
{
    public class HttpRequestStorage : SitkaHttpRequestStorage
    {
        static HttpRequestStorage()
        {
            LtInfoEntityTypeLoaderFactoryFunction = () => MakeNewContext(false);
        }

        protected override List<string> BackingStoreKeys
        {
            get { return new List<string>(); }
        }

        public static Person Person
        {
            get { return GetValueOrDefault(PersonKey, () => KeystoneClaimsHelpers.GetOpenIDUserFromPrincipal(Thread.CurrentPrincipal, Person.GetAnonymousSitkaUser(), DatabaseEntities.People.GetPersonByPersonGuid)); }
            set { SetValue(PersonKey, value); }
        }

        public static Tenant Tenant
        {
            get
            {
                return GetValueOrDefault(TenantKey,
                    () =>
                    {
                        var httpContext = HttpContext.Current;
                        if (httpContext != null)
                        {
                            var urlHost = httpContext.Request.Url.Host;
                            var tenant = Tenant.All.SingleOrDefault(x => urlHost.Equals(NeptuneWebConfiguration.NeptuneEnvironment.GetCanonicalHostNameForEnvironment(x), StringComparison.InvariantCultureIgnoreCase));
                            Check.RequireNotNull(tenant, string.Format("Could not determine tenant from host {0}", urlHost));
                            return tenant;
                        }
                        else
                        {
                            return Tenant.SitkaTechnologyGroup;
                        }
                    });
            }
        }


        public static DatabaseEntities DatabaseEntities
        {
            get { return (DatabaseEntities) LtInfoEntityTypeLoader; }
        }

        private static DatabaseEntities MakeNewContext(bool autoDetectChangesEnabled)
        {
            var databaseEntities = new DatabaseEntities();
            databaseEntities.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            return databaseEntities;
        }

        public static void StartContextForTest()
        {
            var context = MakeNewContext(true);
            SetValue(TenantKey, Tenant.SitkaTechnologyGroup);
            SetValue(DatabaseContextKey, context);
        }

        public static void EndContextForTest()
        {
            if (!BackingStore.Contains(DatabaseContextKey))
            {
                return;
            }
            var databaseEntities = BackingStore[DatabaseContextKey] as DatabaseEntities;
            if (databaseEntities != null)
            {
                databaseEntities.Dispose();
                BackingStore[DatabaseContextKey] = null;
            }
            BackingStore.Remove(DatabaseContextKey);

            if (!BackingStore.Contains(TenantKey))
            {
                return;
            }
            var tenant = BackingStore[TenantKey] as Tenant;
            if (tenant != null)
            {
                BackingStore[TenantKey] = null;
            }
            BackingStore.Remove(TenantKey);

            if (!BackingStore.Contains(PersonKey))
            {
                return;
            }
            var person = BackingStore[PersonKey] as Person;
            if (person != null)
            {
                BackingStore[PersonKey] = null;
            }
            BackingStore.Remove(PersonKey);
        }
    }
}
