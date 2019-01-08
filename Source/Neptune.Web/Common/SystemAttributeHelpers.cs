/*-----------------------------------------------------------------------
<copyright file="MultiTenantHelpers.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.Spatial;
using System.Linq;
using Neptune.Web.Controllers;
// ReSharper disable PossibleNullReferenceException

namespace Neptune.Web.Common
{

    // This was an ugly hack to spare some tedium during the process of dropping multitenancy. These attributes should all be eventually moved to the config file, but for now will continue to live in the DB.
    public static class SystemAttributeHelpers
    {
        private static readonly EnglishPluralizationService PluralizationService = new EnglishPluralizationService();
        
        public static string GetTenantDisplayName()
        {
            return HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().TenantDisplayName;
        }

        public static string GetToolDisplayName()
        {
            return HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().ToolDisplayName;
        }

        public static string GetTenantSquareLogoUrl()
        {
            return HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().TenantSquareLogoFileResource != null
                ? HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().TenantSquareLogoFileResource.GetFileResourceUrl()
                : "/Content/img/Neptune_Logo_Square.png";
        }

        public static string GetTenantBannerLogoUrl()
        {
            return HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().TenantBannerLogoFileResource != null
                ? HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().TenantBannerLogoFileResource.GetFileResourceUrl()
                : "/Content/img/Neptune_Logo_2016_FNL.width-600.png";
        }

        public static string GetTenantStyleSheetUrl()
        {
            return HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().TenantStyleSheetFileResource != null
                ? new SitkaRoute<HomeController>(c=>c.Style()).BuildUrlFromExpression()
                : "~/Content/Bootstrap/neptune/base.theme.min.css";
        }
        
        public static DbGeometry GetDefaultBoundingBox()
        {
            return HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().DefaultBoundingBox;
        }

        public static int GetMinimumYear()
        {
            return HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().MinimumYear;
        }

        public static string GetTenantRecaptchaPrivateKey()
        {
            return HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().RecaptchaPrivateKey;
        }

        public static string GetTenantRecaptchaPublicKey()
        {
            return HttpRequestStorage.DatabaseEntities.SystemAttributes.SingleOrDefault().RecaptchaPublicKey;
        }

        public static string GetTenantName()
        {
            return "OCStormwater";
        }
    }
}
