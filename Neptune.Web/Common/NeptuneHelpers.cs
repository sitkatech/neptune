/*-----------------------------------------------------------------------
<copyright file="NeptuneHelpers.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Drawing;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Neptune.Web.Controllers;

namespace Neptune.Web.Common
{
    public static class NeptuneHelpers
    {
        public static readonly List<string> DefaultColorRange = new List<string>
        {
            "#1f77b4",
            "#ff7f0e",
            "#aec7e8",
            "#ffbb78",
            "#2ca02c",
            "#98df8a",
            "#d62728",
            "#ff9896",
            "#9467bd",
            "#c5b0d5",
            "#022c99",
            "#507e3c",
            "#0d5875",
            "#37aba8",
            "#44cc44",
            "#afa5f2",
            "#d3ffce",
            "#070a41"
        };

        public static string GenerateLogInUrlWithReturnUrl(HttpContext httpContext, LinkGenerator linkGenerator, string canonicalHostName)
        {
            var returnUrl = httpContext.Request.GetDisplayUrl();
            //var link = new Uri(linkGenerator.GetUriByAction(context, "Login", "Account", new { returnUrl }));
            //return link.ToString();
            var logInUrl = SitkaRoute<AccountController>.BuildUrlFromExpression(linkGenerator, c => c.Login());
            return OnErrorOrNotFoundPage(httpContext, linkGenerator, canonicalHostName, returnUrl) ? logInUrl : $"{logInUrl}?returnUrl={HttpUtility.UrlEncode(returnUrl)}";
        }

        public static string GenerateLogInUrlWithReturnUrl(HttpContext httpContext)
        {
            var returnUrl = httpContext.Request.GetDisplayUrl();
            return GenerateLogInUrlWithReturnUrl(returnUrl, httpContext);
        }

        public static string GenerateLogInUrlWithReturnUrl(string returnUrl, HttpContext httpContext)
        {
            var linkGenerator = httpContext.RequestServices.GetService<LinkGenerator>();
            return GenerateLogInUrlWithReturnUrl(httpContext, linkGenerator, "");
        }

        public static string GenerateLogOutUrl()
        {
            // LogOff is an async route so we can't use a Sitka route
            return "/Account/LogOff";
        }

        private static bool OnErrorOrNotFoundPage(HttpContext httpContext, LinkGenerator linkGenerator, string canonicalHostName, string url)
        {
            var returnUrlPathAndQuery = new Uri(url).PathAndQuery;
            var notFoundUrlPathAndQuery = new Uri(SitkaRoute<HomeController>.BuildAbsoluteUrlHttpsFromExpression(linkGenerator, "https://" + canonicalHostName, x => x.NotFound())).PathAndQuery;
            var errorUrlPathAndQuery = new Uri(SitkaRoute<HomeController>.BuildAbsoluteUrlHttpsFromExpression(linkGenerator, "https://" + canonicalHostName, x => x.Error())).PathAndQuery;
            var onErrorOrNotFoundPage = returnUrlPathAndQuery.StartsWith(notFoundUrlPathAndQuery, StringComparison.InvariantCultureIgnoreCase) ||
                                        returnUrlPathAndQuery.StartsWith(errorUrlPathAndQuery, StringComparison.InvariantCultureIgnoreCase);
            return onErrorOrNotFoundPage;
        }

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color ChangeColorBrightness(this Color color, float correctionFactor)
        {
            var red = (float)color.R;
            var green = (float)color.G;
            var blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        // see: https://stackoverflow.com/questions/6848296/how-do-i-serialize-an-object-into-query-string-format
        public static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
    }
}
