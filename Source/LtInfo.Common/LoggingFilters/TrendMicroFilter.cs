﻿/*-----------------------------------------------------------------------
<copyright file="TrendMicroFilter.cs" company="Sitka Technology Group">
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
using System;
using System.Text.RegularExpressions;

namespace LtInfo.Common.LoggingFilters
{
    /// <summary>
    /// Trendmicro antivirus is making calls into the application. Seems to follow along on various URLs.
    /// The problem is mostly that it ends up doing GET on POST urls, and that it doesn't often have the
    /// full context to make a request successfully. So we filter out these errors.
    /// </summary>
    public class TrendMicroFilter : ISitkaLoggingFilter
    {
        public bool ShouldRequestBeFiltered(SitkaRequestInfo requestInfo)
        {
            return (
                       // Via: 1.0 wtp-g3-maya1.sjdc:3128 (squid/2.6.STABLE21)
                       Regex.IsMatch(requestInfo.DebugInfo.RequestInfo, @"^Via:.*\.sjdc", RegexOptions.Multiline) ||
                       // Via: 1.0 wtp-gd-maya7.iad1:3128 (squid/2.6.STABLE21)       
                       Regex.IsMatch(requestInfo.DebugInfo.RequestInfo, @"^Via:.*\.iad1", RegexOptions.Multiline) ||
                       // Via: 1.0 wtp-go-maya1.sjdc.dcsecct.trendmicro.com:3128 (squid/2.6.STABLE21)
                       Regex.IsMatch(requestInfo.DebugInfo.RequestInfo, @"^Via:.*\.trendmicro\.com", RegexOptions.Multiline) ||
                       // Hostname: 150-70-64-214.trendmicro.com
                       requestInfo.DebugInfo.Hostname.EndsWith("trendmicro.com", StringComparison.InvariantCultureIgnoreCase) ||
                       // Hostname: 150-70-64-214.trendmicro.org
                       requestInfo.DebugInfo.Hostname.EndsWith("trendnet.org", StringComparison.InvariantCultureIgnoreCase) ||
                       // Hostname: xxxx.sjdc
                       requestInfo.DebugInfo.Hostname.EndsWith(".sjdc", StringComparison.InvariantCultureIgnoreCase) ||
                       // Hostname: wtp-gd-maya1.iad1
                       requestInfo.DebugInfo.Hostname.EndsWith(".iad1", StringComparison.InvariantCultureIgnoreCase) ||
                       requestInfo.DebugInfo.WhoIsInfo.Contains("trendmicro", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
