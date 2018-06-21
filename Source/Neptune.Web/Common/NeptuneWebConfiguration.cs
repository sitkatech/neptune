﻿/*-----------------------------------------------------------------------
<copyright file="NeptuneWebConfiguration.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.IO;
using System.Linq;
using System.Web.Configuration;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    public class NeptuneWebConfiguration : SitkaWebConfiguration
    {
        public static readonly int MaximumAllowedUploadFileSize = Int32.Parse(SitkaConfiguration.GetRequiredAppSetting("MaximumAllowedUploadFileSize"));
        public static readonly string DatabaseConnectionString = SitkaConfiguration.GetRequiredAppSetting("DatabaseConnectionString");
        public static readonly string RecaptchaValidatorUrl = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("RecaptchaValidatorUrl");
        public static readonly string SitkaSupportEmail = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("SitkaSupportEmail");
        public static readonly string DoNotReplyEmail = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("DoNotReplyEmail");
        public static readonly string Ogr2OgrExecutable = SitkaConfiguration.GetRequiredAppSetting("Ogr2OgrExecutable");
        public static readonly string OgrInfoExecutable = SitkaConfiguration.GetRequiredAppSetting("OgrInfoExecutable");
        public static readonly int ReportingPeriodStartMonth = Int32.Parse(SitkaConfiguration.GetRequiredAppSetting("ReportingPeriodStartMonth"));
        public static readonly int ReportingPeriodStartDay = Int32.Parse(SitkaConfiguration.GetRequiredAppSetting("ReportingPeriodStartDay"));
        public static readonly int DefaultSupportPersonID = Int32.Parse(SitkaConfiguration.GetRequiredAppSetting("DefaultSupportPersonID"));

        public static readonly TimeSpan HttpRuntimeExecutionTimeout = ((HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime")).ExecutionTimeout;       
        public static readonly string KeystoneUrl = SitkaConfiguration.GetRequiredAppSetting("KeystoneUrl");
        public static readonly string KeystoneRegisterUrl = SitkaConfiguration.GetRequiredAppSetting("KeystoneRegisterUrl");
        public static readonly string KeystoneInviteUserUrl = SitkaConfiguration.GetRequiredAppSetting("KeystoneInviteUserUrl");
        public static readonly string KeystoneUserProfileUrl = SitkaConfiguration.GetRequiredAppSetting("KeystoneUserProfileUrl");
        public static readonly string KeystoneOpenIDClientId = SitkaConfiguration.GetRequiredAppSetting("KeystoneOpenIDClientId");
        public static readonly string KeystoneOpenIDUrl = SitkaConfiguration.GetRequiredAppSetting("KeystoneOpenIDUrl");
        public static readonly string KeystoneOpenIDClientSecret = SitkaConfiguration.GetRequiredAppSetting("KeystoneOpenIDClientSecret");
        public static readonly string ParcelMapServiceUrl = SitkaConfiguration.GetRequiredAppSetting("ParcelMapServiceUrl");

        public static readonly DirectoryInfo LogFileFolder = ParseLogFileFolder();

        public static readonly NeptuneEnvironment NeptuneEnvironment = NeptuneEnvironment.MakeNeptuneEnvironment(SitkaConfiguration.GetRequiredAppSetting("NeptuneEnvironment"));

        public static readonly string CanonicalHostName = CanonicalHostNames.FirstOrDefault();

        public static List<string> CanonicalHostNames => Tenant.All.OrderBy(x => x.TenantID).Select(x => NeptuneEnvironment.GetCanonicalHostNameForEnvironment(x)).ToList();

        public static string GetCanonicalHost(string hostName, bool useApproximateMatch)
        {
            //First search for perfect match
            var canonicalHostNames = CanonicalHostNames;
            var result = canonicalHostNames.FirstOrDefault(h => string.Equals(h, hostName, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrWhiteSpace(result) || !useApproximateMatch)
            {
                return result;
            }

            //Use the domain name  (laketahoeinfo.org -->  should use www.laketahoeinfo.org for the match)
            return canonicalHostNames.FirstOrDefault(h => h.EndsWith(hostName, StringComparison.InvariantCultureIgnoreCase)) ?? CanonicalHostName;
        }



        private static DirectoryInfo ParseLogFileFolder()
        {
            const string appSettingKeyName = "LogFileFolder";
            var logFileFolder = SitkaConfiguration.GetRequiredAppSetting(appSettingKeyName);
            Check.RequireDirectoryExists(logFileFolder, "App setting {0} must be a folder that exists, folder does not exist.");
            return new DirectoryInfo(logFileFolder);
        }
    }

}
