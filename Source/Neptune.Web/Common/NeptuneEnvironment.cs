/*-----------------------------------------------------------------------
<copyright file="NeptuneEnvironment.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common;

namespace Neptune.Web.Common
{
    /// <summary>
    ///     Encapsulates Neptune Environmental specific differences
    /// </summary>
    public abstract class NeptuneEnvironment
    {
        public abstract bool IsUnitTestWebServiceTokenOkInThisEnvironment { get; }
        public abstract NeptuneEnvironmentType NeptuneEnvironmentType { get; }

        public abstract string DomainPrefix { get; }

        public abstract IEnumerable<string> GetCanonicalHostNamesForEnvironment();

        public static NeptuneEnvironment MakeNeptuneEnvironment(string neptuneEnvironmentSetting)
        {
            var neptuneEnvironmentType = neptuneEnvironmentSetting.ParseAsEnum<NeptuneEnvironmentType>();
            switch (neptuneEnvironmentType)
            {
                case NeptuneEnvironmentType.Local:
                    return new NeptuneEnvironmentLocal();
                case NeptuneEnvironmentType.Qa:
                    return new NeptuneEnvironmentQa();
                case NeptuneEnvironmentType.Prod:
                    return new NeptuneEnvironmentProd();
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Unknown {typeof(NeptuneEnvironmentType).Name} {neptuneEnvironmentType}");
            }
        }

        private class NeptuneEnvironmentLocal : NeptuneEnvironment
        {
            // OK in local because this is where we run unit tests
            public override bool IsUnitTestWebServiceTokenOkInThisEnvironment => true;

            public override NeptuneEnvironmentType NeptuneEnvironmentType => NeptuneEnvironmentType.Local;
            public override string DomainPrefix => "localhost";

            public override IEnumerable<string> GetCanonicalHostNamesForEnvironment()
            {
                var canonicalHostNames = SitkaConfiguration.GetRequiredAppSettingList("CanonicalHostName");
                return canonicalHostNames;
            }
        }


        private class NeptuneEnvironmentProd : NeptuneEnvironment
        {
            // Definitely not OK in Prod, no unit testing here would be a security hole
            public override bool IsUnitTestWebServiceTokenOkInThisEnvironment => false;

            public override NeptuneEnvironmentType NeptuneEnvironmentType => NeptuneEnvironmentType.Prod;
            public override string DomainPrefix => "www";

            public override IEnumerable<string> GetCanonicalHostNamesForEnvironment()
            {
                var canonicalHostNames = SitkaConfiguration.GetRequiredAppSettingList("CanonicalHostName");
                return canonicalHostNames;
            }
        }

        private class NeptuneEnvironmentQa : NeptuneEnvironment
        {
            // Not OK in QA, no unit testing here
            public override bool IsUnitTestWebServiceTokenOkInThisEnvironment => true;

            public override NeptuneEnvironmentType NeptuneEnvironmentType => NeptuneEnvironmentType.Qa;
            public override string DomainPrefix => "qa";

            public override IEnumerable<string> GetCanonicalHostNamesForEnvironment()
            {
                var canonicalHostNames = SitkaConfiguration.GetRequiredAppSettingList("CanonicalHostName");
                return canonicalHostNames;
            }
        }
    }


    /// <summary>
    ///     Type enum for the environment name
    /// </summary>
    public enum NeptuneEnvironmentType
    {
        Local,
        Qa,
        Prod
    }
}
