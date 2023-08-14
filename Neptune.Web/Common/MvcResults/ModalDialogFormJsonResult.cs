/*-----------------------------------------------------------------------
<copyright file="ModalDialogFormJsonResult.cs" company="Sitka Technology Group">
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

using Microsoft.AspNetCore.Mvc;

namespace Neptune.Web.Common.MvcResults
{
    public class ModalDialogFormJsonResult : JsonResult
    {
        public ModalDialogFormJsonResult(bool success, string? redirectUrl, string? onSuccessJavascriptFunctionToRun) : base(new ModalDialogFormJsonData(success, redirectUrl, onSuccessJavascriptFunctionToRun))
        {
        }

        public ModalDialogFormJsonResult(string? redirectUrl) : this(true, redirectUrl, null)
        {
        }

        public ModalDialogFormJsonResult() : this(true, null, null)
        {
        }

        public class ModalDialogFormJsonData
        {
            public readonly bool Success;
            public readonly string? RedirectUrl;
            public readonly string? OnSuccessJavascriptFunctionToRun;

            public ModalDialogFormJsonData(bool success, string? redirectUrl, string? onSuccessJavascriptFunctionToRun)
            {
                Success = success;
                RedirectUrl = redirectUrl;
                OnSuccessJavascriptFunctionToRun = onSuccessJavascriptFunctionToRun;
            }
        }
    }
}
