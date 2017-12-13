﻿/*-----------------------------------------------------------------------
<copyright file="SitkaDisplayExceptionWith503ServiceUnavailable503LoggingFilter.cs" company="Sitka Technology Group">
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
using System.Net;

namespace LtInfo.Common.LoggingFilters
{
    public class SitkaDisplayExceptionWith503ServiceUnavailable503LoggingFilter : ISitkaLoggingFilter
    {
        public bool ShouldRequestBeFiltered(SitkaRequestInfo requestInfo)
        {
            // Should be filtered if:
            // - This is a display exception with a HTTP code
            // - that HTTP code is 503 - Service Unavailable
            if (requestInfo.OriginalException is SitkaDisplayErrorExceptionWithHttpCode)
            {
                var exception = (SitkaDisplayErrorExceptionWithHttpCode)requestInfo.OriginalException;
                return (exception.HttpStatusCode == HttpStatusCode.ServiceUnavailable);
            }

            return false;
        }
    }
}
