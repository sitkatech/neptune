/*-----------------------------------------------------------------------
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
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Configuration;

namespace Neptune.Web.Common
{
    public class NeptuneWebConfiguration : SitkaWebConfiguration
    {
        public static readonly int MaximumAllowedUploadFileSize = Int32.Parse(SitkaConfiguration.GetRequiredAppSetting("MaximumAllowedUploadFileSize"));
        public static readonly string DatabaseConnectionString = SitkaConfiguration.GetRequiredAppSetting("DatabaseConnectionString");
        public static readonly string RecaptchaPublicKey = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("RecaptchaPublicKey");
        public static readonly string RecaptchaPrivateKey = SitkaConfiguration.GetRequiredAppSettingNotNullNotEmptyNotWhitespace("RecaptchaPrivateKey");
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
        public static readonly string ParcelLayerName = SitkaConfiguration.GetOptionalAppSetting("MapServiceLayerNameParcel");
        public static readonly string LandUseBlockLayerName = SitkaConfiguration.GetOptionalAppSetting("MapServiceLayerNameLandUseBlock");
        public static readonly string RegionalSubbasinLayerName = SitkaConfiguration.GetOptionalAppSetting("MapServiceLayerNameRegionalSubbasin");

        public static readonly string AutoDelineateServiceUrl = SitkaConfiguration.GetOptionalAppSetting("AutoDelineateServiceUrl");
        public static readonly string HRUServiceBaseUrl = SitkaConfiguration.GetOptionalAppSetting("HRUServiceBaseUrl");
        public static readonly string ModelBasinServiceUrl = SitkaConfiguration.GetOptionalAppSetting("ModelBasinServiceUrl");
        public static readonly string PrecipitationZoneServiceUrl = SitkaConfiguration.GetOptionalAppSetting("PrecipitationZoneServiceUrl");
        public static readonly string RegionalSubbasinServiceUrl = SitkaConfiguration.GetRequiredAppSetting("RegionalSubbasinServiceUrl");

        public static readonly string PathToPyqgisTestScript = SitkaConfiguration.GetOptionalAppSetting("PathToPyqgisTestScript");
        public static readonly string PyqgisWorkingDirectory = SitkaConfiguration.GetOptionalAppSetting("PyqgisWorkingDirectory");
        public static string PathToPyqgisLauncher = SitkaConfiguration.GetOptionalAppSetting("PathToPyqgisLauncher");

        public static string NereidUrl = SitkaConfiguration.GetOptionalAppSetting("NereidUrl");

        public static string PathToFieldVisitUploadTemplate =
            SitkaConfiguration.GetRequiredAppSetting("PathToFieldVisitUploadTemplate");

        public static readonly DirectoryInfo LogFileFolder = ParseLogFileFolder();
        public static readonly DirectoryInfo NereidLogFileFolder = new DirectoryInfo(Path.Combine(NeptuneWebConfiguration.LogFileFolder.FullName, "NereidLogs"));

        public static readonly NeptuneEnvironment NeptuneEnvironment = NeptuneEnvironment.MakeNeptuneEnvironment(SitkaConfiguration.GetRequiredAppSetting("NeptuneEnvironment"));

        public static readonly string CanonicalHostName = CanonicalHostNames.FirstOrDefault();

        public static readonly string CanonicalHostNameRoot =
            SitkaConfiguration.GetRequiredAppSetting("CanonicalHostNameRoot");
        public static readonly string CanonicalHostNameTrash =
            SitkaConfiguration.GetRequiredAppSetting("CanonicalHostNameTrash");
        public static readonly string CanonicalHostNameModeling =
            SitkaConfiguration.GetRequiredAppSetting("CanonicalHostNameModeling");

        public static List<string> CanonicalHostNames => new List<string>(SitkaConfiguration.GetRequiredAppSettingList("CanonicalHostName"));


        public static readonly string NominatimApiKey = SitkaConfiguration.GetRequiredAppSetting("NominatimAPIKey");

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
