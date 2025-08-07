﻿/*-----------------------------------------------------------------------
<copyright file="ModalDialogFormHelper.cs" company="Sitka Technology Group">
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

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.WebMvc.Common.BootstrapWrappers;
using System.Text.Encodings.Web;

namespace Neptune.WebMvc.Common.ModalDialog
{
    public static class ModalDialogFormHelper
    {
        public const string SaveButtonID = "ltinfo-modal-dialog-save-button-id";
        public const int DefaultDialogWidth = 800;

        /// <summary>
        ///  Creates a link that will open a jQuery UI dialog form.
        /// </summary>
        /// <param name="linkText">The inner text of the anchor element</param>
        /// <param name="dialogContentUrl">The url that will return the content to be loaded into the dialog window</param>
        /// <param name="dialogTitle">The title to be displayed in the dialog window</param>
        /// <param name="dialogWidth">width in pixels of dialog</param>
        /// <param name="saveButtonText">Text for the save button</param>
        /// <param name="cancelButtonText">Text for the cancel button</param>
        /// <param name="extraCssClasses">Any extra css classes for the button</param>
        /// <param name="onJavascriptReadyFunction">Optional javascript function to run when dialog is loaded</param>
        /// <param name="postData">Optional; if provided, will switch the dialog load to a POST from a GET</param>
        /// <returns></returns>
        public static IHtmlContent ModalDialogFormLink(string linkText,
            string dialogContentUrl,
            string dialogTitle,
            int? dialogWidth,
            string saveButtonText,
            string cancelButtonText,
            List<string> extraCssClasses,
            string? onJavascriptReadyFunction,
            string? postData)
        {
            return ModalDialogFormLink(null,
                new HtmlString(linkText),
                dialogContentUrl,
                dialogTitle,
                dialogWidth,
                SaveButtonID,
                saveButtonText,
                cancelButtonText,
                extraCssClasses,
                onJavascriptReadyFunction,
                postData,
                null,
                null);
        }

        /// <summary>
        ///  Creates a link that will open a jQuery UI dialog form.
        /// </summary>
        /// <param name="linkText">The inner text of the anchor element</param>
        /// <param name="dialogContentUrl">The url that will return the content to be loaded into the dialog window</param>
        /// <param name="dialogTitle">The title to be displayed in the dialog window</param>
        /// <param name="dialogWidth">width in pixels of dialog</param>
        /// <param name="saveButtonText">Text for the save button</param>
        /// <param name="cancelButtonText">Text for the cancel button</param>
        /// <param name="extraCssClasses">Any extra css classes for the button</param>
        /// <param name="onJavascriptReadyFunction">Optional javascript function to run when dialog is loaded</param>
        /// <param name="postData">Optional; if provided, will switch the dialog load to a POST from a GET</param>
        /// <returns></returns>
        public static IHtmlContent ModalDialogFormLink(IHtmlContent linkText,
            string dialogContentUrl,
            string dialogTitle,
            int? dialogWidth,
            string saveButtonText,
            string cancelButtonText,
            List<string> extraCssClasses,
            string? onJavascriptReadyFunction,
            string? postData)
        {
            return ModalDialogFormLink(null,
                linkText,
                dialogContentUrl,
                dialogTitle,
                dialogWidth,
                SaveButtonID,
                saveButtonText,
                cancelButtonText,
                extraCssClasses,
                onJavascriptReadyFunction,
                postData,
                null,
                null);
        }

        /// <summary>
        ///     Creates a link that will open a jQuery UI dialog form.
        ///     Adds additional parameters controlling button IDs if needed.
        /// </summary>
        /// <param name="linkID">Optional LinkID to be able to access it later on the page</param>
        /// <param name="linkText">The inner text of the anchor element</param>
        /// <param name="dialogContentUrl">The url that will return the content to be loaded into the dialog window</param>
        /// <param name="dialogTitle">The title to be displayed in the dialog window</param>
        /// <param name="dialogWidth">width in pixels of dialog</param>
        /// <param name="saveButtonID">ID for the save button for later reference by jQuery, etc. Take care to make unique!</param>
        /// <param name="saveButtonText">Text for the save button</param>
        /// <param name="cancelButtonText">Text for the cancel button</param>
        /// <param name="extraCssClasses">Any extra css classes for the button</param>
        /// <param name="onJavascriptReadyFunction">Optional javascript function to run when dialog is loaded</param>
        /// <param name="postData">Optional; if provided, will switch the dialog load to a POST from a GET</param>
        /// <param name="optionalDialogFormID"></param>
        /// <returns></returns>
        public static IHtmlContent ModalDialogFormLink(string linkID,
            IHtmlContent linkText,
            string dialogContentUrl,
            string dialogTitle,
            int? dialogWidth,
            string saveButtonID,
            string saveButtonText,
            string cancelButtonText,
            List<string> extraCssClasses,
            string? onJavascriptReadyFunction,
            string? postData,
            string? optionalDialogFormID)
        {
            return ModalDialogFormLink(linkID, linkText, dialogContentUrl, dialogTitle, dialogWidth, saveButtonID,
                saveButtonText, cancelButtonText, extraCssClasses, onJavascriptReadyFunction, postData,
                optionalDialogFormID, null);
        }

        /// <summary>
        ///     Creates a link that will open a jQuery UI dialog form.
        ///     Adds additional parameters controlling button IDs if needed.
        /// </summary>
        /// <param name="linkID">Optional LinkID to be able to access it later on the page</param>
        /// <param name="linkText">The inner text of the anchor element</param>
        /// <param name="dialogContentUrl">The url that will return the content to be loaded into the dialog window</param>
        /// <param name="dialogTitle">The title to be displayed in the dialog window</param>
        /// <param name="dialogWidth">width in pixels of dialog</param>
        /// <param name="saveButtonID">ID for the save button for later reference by jQuery, etc. Take care to make unique!</param>
        /// <param name="saveButtonText">Text for the save button</param>
        /// <param name="cancelButtonText">Text for the cancel button</param>
        /// <param name="extraCssClasses">Any extra css classes for the button</param>
        /// <param name="onJavascriptReadyFunction">Optional javascript function to run when dialog is loaded</param>
        /// <param name="postData">Optional; if provided, will switch the dialog load to a POST from a GET</param>
        /// <param name="optionalDialogFormID"></param>
        /// <param name="hoverText"></param>
        /// <returns></returns>
        public static IHtmlContent ModalDialogFormLink(string linkID,
            IHtmlContent linkText,
            string dialogContentUrl,
            string dialogTitle,
            int? dialogWidth,
            string saveButtonID,
            string saveButtonText,
            string cancelButtonText,
            List<string> extraCssClasses,
            string? onJavascriptReadyFunction,
            string? postData,
            string? optionalDialogFormID,
            string hoverText)
        {
            var anchorTag = new TagBuilder("a");
            anchorTag.InnerHtml.AppendHtml(linkText);
            if (!string.IsNullOrWhiteSpace(linkID))
            {
                anchorTag.Attributes.Add("id", linkID);
            }
            anchorTag.Attributes.Add("href", dialogContentUrl);
            anchorTag.Attributes.Add("data-dismiss", "modal");
            anchorTag.Attributes.Add("data-dialog-title", dialogTitle);
            anchorTag.Attributes.Add("data-dialog-width", dialogWidth.ToString());

            if (!string.IsNullOrWhiteSpace(saveButtonID))
            {
                anchorTag.Attributes.Add("data-save-button-id", saveButtonID);
            }
            if (!string.IsNullOrWhiteSpace(saveButtonText))
            {
                anchorTag.Attributes.Add("data-save-button-text", saveButtonText);
            }
            anchorTag.Attributes.Add("data-cancel-button-text", cancelButtonText);

            
            if (!string.IsNullOrWhiteSpace(optionalDialogFormID))
            {
                anchorTag.Attributes.Add("data-optional-dialog-form-id", optionalDialogFormID);
            }

            var javascripReadyFunctionAsParameter = !string.IsNullOrWhiteSpace(onJavascriptReadyFunction) ? $"function() {{{onJavascriptReadyFunction}();}}" : "null";
            var postDataAsParameter = !string.IsNullOrWhiteSpace(postData) ? postData : "null";
            var onclickFunction = $"return modalDialogLink(this, {javascripReadyFunctionAsParameter}, {postDataAsParameter});";
            anchorTag.Attributes.Add("onclick", onclickFunction);

            if (extraCssClasses != null)
            {
                foreach (var extraCssClass in extraCssClasses)
                {
                    anchorTag.AddCssClass(extraCssClass);
                }
            }

            if (!string.IsNullOrWhiteSpace(hoverText))
            {
                anchorTag.Attributes.Add("title", hoverText);
            }

            var writer = new StringWriter();
            var builder = new HtmlContentBuilder();
            builder.AppendFormat("{0}", anchorTag);
            builder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        public static IHtmlContent ModalDialogFormLink(string? linkText, string dialogUrl, string dialogTitle, List<string> extraCssClasses, bool hasPermission)
        {
            return hasPermission ? ModalDialogFormLink(new HtmlString(linkText), dialogUrl, dialogTitle, DefaultDialogWidth, "Save", "Cancel", extraCssClasses, null, null) : new HtmlString(string.Empty);
        }

        public static IHtmlContent ModalDialogFormLink(string linkText, string dialogUrl, string dialogTitle, int dialogWidth, bool hasPermission, string? dialogFormID)
        {
            return hasPermission
                ? ModalDialogFormLink(null, new HtmlString(linkText), dialogUrl, dialogTitle, dialogWidth, SaveButtonID, "Save", "Cancel", new List<string>(), null, null, dialogFormID, null)
                : new HtmlString(string.Empty);
        }

        public static IHtmlContent MakeDeleteLink(string? linkText, string deleteDialogUrl, List<string> extraCssClasses, bool userHasDeletePermission)
        {
            return userHasDeletePermission ? ModalDialogFormLink(new HtmlString(linkText), deleteDialogUrl, "Confirm Delete", 500, "Delete", "Cancel", extraCssClasses, null, null) : new HtmlString(string.Empty);
        }

        public static IHtmlContent MakeEditIconLink(string dialogUrl, string dialogTitle, bool hasPermission)
        {
            return MakeEditIconLink(dialogUrl, dialogTitle, DefaultDialogWidth, hasPermission);
        }

        public static IHtmlContent MakeEditIconLink(string dialogUrl, string dialogTitle, int width, bool hasPermission)
        {
            return hasPermission ? ModalDialogFormLink(null, BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-edit"), dialogUrl, dialogTitle, width, SaveButtonID, "Save", "Cancel", new List<string>(), null, null, null) : new HtmlString(string.Empty);
        }
    }
}
