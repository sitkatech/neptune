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
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Web;
using System.Xml;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;

namespace LtInfo.Common.Views
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

        public static string CheckedIfEqual(int? value, int testValue)
        {
            return (value.HasValue && testValue == value.Value).ToCheckedOrEmpty();
        }

        public static string CheckedIfEqual(bool? value, bool testValue)
        {
            return (value.HasValue && testValue == value.Value).ToCheckedOrEmpty();
        }

        public static string Prune(this string value, int totalLength)
        {
            if (String.IsNullOrEmpty(value))
                return value;

            if (value.Length < totalLength)
                return value;

            return String.Format("{0}...", value.Substring(0, totalLength - 3));
        }

        public static string Flatten(this string value, string replacement)
        {
            return String.IsNullOrEmpty(value) ? value : value.Replace("\r\n", replacement).Replace("\n", replacement).Replace("\r", replacement);
        }

        public static string Flatten(this string value)
        {
            return Flatten(value, " ");
        }

        public static string HtmlEncode(this string value)
        {
            return String.IsNullOrEmpty(value) ? value : HttpUtility.HtmlEncode(value);
        }

        public static string HtmlEncodeWithBreaks(this string value)
        {
            var ret = value.HtmlEncode();
            return String.IsNullOrEmpty(ret) ? ret : ret.Replace("\r\n","\n").Replace("\r","\n").Replace("\n", "<br/>\r\n");
        }

        public static string DataTableToXmlRowCol(this DataTable table, GridSpec<IStringIndexer> spec)
        {
            var rowID = 0;

            using (var stream = new MemoryStream())
            using (var writer = new XmlTextWriter(stream, null))
            {
                writer.WriteStartElement("rows");
                var reader = table.CreateDataReader();

                while (reader.Read())
                {
                    var indexer = AdaptReaderToIStringIndexer(reader);
                    indexer.ToXmlRowCell(writer, rowID, spec);
                    rowID++;
                }

                reader.Close();
                writer.WriteFullEndElement();
                writer.Flush();

                var array = stream.ToArray();
                var s = Encoding.UTF8.GetString(array);

                return s;
            }
        }

        public static void ToXmlRowCell<T>(this T thingToRead, XmlTextWriter writer, int rowID, GridSpec<T> gridSpec)
        {
            writer.WriteStartElement("row");
            writer.WriteAttributeString("id", rowID.ToString(CultureInfo.InvariantCulture));

            foreach (var columnSpec in gridSpec)
            {
                writer.WriteStartElement("cell");

                var cellCssClass = columnSpec.CalculateCellCssClass(thingToRead);
                var title = columnSpec.CalculateTitle(thingToRead);

                if (!String.IsNullOrEmpty(cellCssClass))
                {
                    writer.WriteAttributeString("class", cellCssClass);
                }

                if (!String.IsNullOrEmpty(title))
                {
                    writer.WriteAttributeString("title", title);
                }

                //if (columnSpec.IsHidden)
                //{
                //    writer.WriteAttributeString("hidden", "true");
                //}

                var value = columnSpec.CalculateStringValue(thingToRead) ?? String.Empty;
                var stripped = XmlResult.StripInvalidCharacters(value);
                var xmlEncoded = SecurityElement.Escape(stripped);
                var translated = XmlResult.XmlEncodeCodePage1252Characters(xmlEncoded);

                writer.WriteRaw(translated);
                writer.WriteFullEndElement();
            }

            writer.WriteFullEndElement();
        }

        private static IStringIndexer AdaptReaderToIStringIndexer(DataTableReader reader)
        {
            return new StringIndexerDataReader(reader);
        }


        public static string DisplayDataLine(Func<object> value)
        {
            return DisplayDataLine(true, value);
        }

        public static string DisplayDataLineDefaultString(string defaultString)
        {
            var result = "<p style=\"color:grey\">" + defaultString + "</p>";
            return result;
        }

        public static string DisplayDataLine(bool predicate, Func<object> stringFuncIfTrue, string stringIfFalse)
        {
            var result = stringIfFalse;

            if (predicate)
            {
                var o = stringFuncIfTrue();
                if (o != null)
                    result = o.ToString();
            }
            return result.HtmlEncode().Flatten("<br/>");
        }

        public static string DisplayDataLine(bool predicate, Func<object> stringFuncIfTrue)
        {
            return DisplayDataLine(predicate, stringFuncIfTrue, NoneString);
        }

        public static string DisplayValue(this int value, string stringIfNullOrDefault)
        {
            return new int?(value).DisplayValue(stringIfNullOrDefault);
        }

        public static string DisplayValue(this int? value, string stringIfNullOrDefault)
        {
            return value == null || value == default(int) ? stringIfNullOrDefault : value.ToString();
        }

        public static string DisplayValue(this int? value)
        {
            return DisplayValue(value, String.Empty);
        }

        public static string DisplayValue(this int value)
        {
            return DisplayValue(value, String.Empty);
        }

        public static string DisplayValue(this DateTime? value, string format)
        {
            return value == null ? String.Empty : value.Value.ToString(format);
        }

        public static string DisplayValue(this Boolean? value)
        {
            return value == null ? String.Empty : value.Value.ToString();
        }
    }
}
