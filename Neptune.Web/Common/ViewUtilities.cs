/*-----------------------------------------------------------------------
<copyright file="ViewUtilities.cs" company="Sitka Technology Group">
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

using System.Web;

namespace Neptune.Web.Common
{
    public static class ViewUtilities
    {
        public const string NoneString = "None";
        public const string NoAnswerProvided = "<No answer provided>";
        public const string NoCommentString = "<no comment>";
        public const string NaString = "n/a";
        public const string NotFoundString = "(not found)";
        public const string NotAvailableString = "Not available";
        public const string NotProvidedString = "not provided";
        public const string NoChangesRecommended = "No changes recommended";

        public static string Prune(this string value, int totalLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length < totalLength)
                return value;

            return $"{value.Substring(0, totalLength - 3)}...";
        }

        public static string Flatten(this string value, string replacement)
        {
            return string.IsNullOrEmpty(value) ? value : value.Replace("\r\n", replacement).Replace("\n", replacement).Replace("\r", replacement);
        }

        public static string Flatten(this string value)
        {
            return Flatten(value, " ");
        }

        public static string HtmlEncode(this string value)
        {
            return string.IsNullOrEmpty(value) ? value : HttpUtility.HtmlEncode(value);
        }

        public static string HtmlEncodeWithBreaks(this string value)
        {
            var ret = value.HtmlEncode();
            return string.IsNullOrEmpty(ret) ? ret : ret.Replace("\r\n","\n").Replace("\r","\n").Replace("\n", "<br/>\r\n");
        }
    }
}
