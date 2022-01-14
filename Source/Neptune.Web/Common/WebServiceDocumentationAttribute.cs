/*-----------------------------------------------------------------------
<copyright file="WebServiceDocumentationAttribute.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class WebServiceDocumentationAttribute : Attribute
    {
        public string Documentation;

        public WebServiceDocumentationAttribute(string formatString, FieldDefinitionTypeEnum fieldDefinitionTypeEnum)
        {
            Documentation = string.Format(formatString, FieldDefinitionType.ToType(fieldDefinitionTypeEnum).GetFieldDefinitionLabel());
        }

        public WebServiceDocumentationAttribute(string formatString, FieldDefinitionTypeEnum fieldDefinitionTypeEnum1, FieldDefinitionTypeEnum fieldDefinitionTypeEnum2)
        {
            Documentation = string.Format(formatString, FieldDefinitionType.ToType(fieldDefinitionTypeEnum1).GetFieldDefinitionLabel(), FieldDefinitionType.ToType(fieldDefinitionTypeEnum2).GetFieldDefinitionLabel());
        }

        public WebServiceDocumentationAttribute(string formatString, FieldDefinitionTypeEnum fieldDefinitionTypeEnum1, FieldDefinitionTypeEnum fieldDefinitionTypeEnum2, FieldDefinitionTypeEnum fieldDefinitionTypeEnum3, FieldDefinitionTypeEnum fieldDefinitionTypeEnum4)
        {
            Documentation = string.Format(formatString, FieldDefinitionType.ToType(fieldDefinitionTypeEnum1).GetFieldDefinitionLabel(), FieldDefinitionType.ToType(fieldDefinitionTypeEnum2).GetFieldDefinitionLabel(), FieldDefinitionType.ToType(fieldDefinitionTypeEnum3).GetFieldDefinitionLabel(), FieldDefinitionType.ToType(fieldDefinitionTypeEnum4).GetFieldDefinitionLabel());
        }

        public WebServiceDocumentationAttribute(string s)
        {
            Documentation = s;
        }
    }
}
