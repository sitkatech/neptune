/*-----------------------------------------------------------------------
<copyright file="CkEditorExtension.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using Neptune.Web.Controllers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Common
{
    public static class CkEditorExtension
    {
        public enum CkEditorToolbar
        {
            All,
            AllOnOneRow,
            AllOnOneRowNoMaximize,
            Minimal,
            MinimalWithImages,
            None
        }

        public static HtmlString CkEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression,
            CkEditorToolbar ckEditorToolbarMode,
            bool editable,
            bool allowAllContent,
            int? height) where TModel : FormViewModel
        {
            return CkEditorFor(helper, expression, ckEditorToolbarMode, editable, allowAllContent, null, height);
        }

        /// <summary>
        /// This is used by ckeditors for a NeptunePage
        /// </summary>
        public static HtmlString CkEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression,
            CkEditorToolbar ckEditorToolbarMode,
            LinkGenerator linkGenerator,
            bool editable,
            int neptunePageID,
            int? height) where TModel : FormViewModel
        {
            var filebrowserImageUploadUrl = SitkaRoute<FileResourceController>.BuildUrlFromExpression(linkGenerator, c => c.CkEditorUploadFileResource(neptunePageID));
            return CkEditorFor(helper, expression, ckEditorToolbarMode, editable, false, filebrowserImageUploadUrl, height);
        }
        
        public static HtmlString CkEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression,
            CkEditorToolbar ckEditorToolbarMode,
            bool editable,
            bool allowAllContent,
            string filebrowserImageUploadUrl,
            int? height) where TModel : FormViewModel
        {
            //var metadata = FromLambdaExpression(expression, helper.ViewData);
            //var modelValue = (HtmlString) metadata.Model;
            //if (!editable)
            //{
            //    return modelValue;
            //}
            var modelID = helper.IdFor(expression).ToString();

            var textAreaID = $"CkEditorFor{modelID}";

            var htmlAttributes = new Dictionary<string, object>() {{"id", textAreaID}, {"contentEditable", "true"}, {"data-cke-editor-id", modelID}};

            var generateJavascript = GenerateJavascript(modelID, ckEditorToolbarMode, filebrowserImageUploadUrl, allowAllContent, height);
            var textAreaHtmlString = helper.TextAreaFor(expression, htmlAttributes);
            //return MvcHtmlString.Create(string.Format(@"{0}{1}", textAreaHtmlString, generateJavascript));
            return new HtmlString($@"{textAreaHtmlString}{generateJavascript}");
        }

        public static string GenerateJavascript(string modelID, CkEditorToolbar ckEditorToolbarMode, string filebrowserImageUploadUrl, bool allowAllContent, int? height)
        {
            var tag = new TagBuilder("script");
            tag.Attributes.Add("type", "text/javascript");
            tag.Attributes.Add("language", "javascript");
            var ckEditorToolbarJavascript = GenerateToolbarSettings(ckEditorToolbarMode);
            var ckEditorID = $"CkEditorFor{modelID}";

            var wireUpJsForImageUploader = string.Empty;
            if (ckEditorToolbarJavascript.HasImageToolbarButton && !string.IsNullOrWhiteSpace(filebrowserImageUploadUrl))
            {
                wireUpJsForImageUploader = $"\r\n           , filebrowserImageUploadUrl: {filebrowserImageUploadUrl.ToJS()}";
            }

            var allowedContentString = allowAllContent ? "\r\n           , allowedContent: true" : string.Empty;

            var heightString = height.HasValue ? $"\r\n           , height: {height.Value}" : string.Empty;

            tag.InnerHtml.Append( $@"
    // <![CDATA[
    jQuery(document).ready(function ()
    {{
        CKEDITOR.replace(""{ckEditorID}"", {{
           toolbar:
           [
{ckEditorToolbarJavascript.JavascriptForToolbar}
           ]{allowedContentString}{wireUpJsForImageUploader}{heightString}
        }});
    }});
    // ]]>
");

            StringBuilder result = new StringBuilder();
            using (var writer = new StringWriter())
            {
                tag.WriteTo(writer, HtmlEncoder.Default);
                result.Append(writer);
            }

            return result.ToString();
        }

        private class CkEditorToolbarJavascript
        {
            public readonly string JavascriptForToolbar;
            public readonly bool HasImageToolbarButton;

            public CkEditorToolbarJavascript(string javascriptForToolbar, bool hasImageToolbarButton)
            {
                JavascriptForToolbar = javascriptForToolbar;
                HasImageToolbarButton = hasImageToolbarButton;
            }
        }

        private static CkEditorToolbarJavascript GenerateToolbarSettings(CkEditorToolbar ckEditorToolbarMode)
        {
            bool hasImageToolbarButton;
            string toolbarSettings;
            switch (ckEditorToolbarMode)
            {
                case CkEditorToolbar.All:
                    toolbarSettings =
                        @"            { name: 'document',    groups: [ 'mode', 'document', 'doctools' ], items: [ 'Source', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', 'Templates', 'document' ] },
            { name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ], items: [ 'Find', 'Replace', 'SelectAll', 'Scayt' ] },
            { name: 'insert', items: [ 'CreatePlaceholder', 'Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe', 'InsertPre' ] },
            { name: 'forms', items: [ 'Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField' ] },
            { name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] },
            '/',
            { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ], items: [ 'Bold', 'Italic', 'Underline', 'Subscript', 'Superscript', 'RemoveFormat' ] },
            { name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align' ], items: [ 'NumberedList', 'BulletedList', 'Outdent', 'Indent', 'CreateDiv', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'BidiLtr', 'BidiRtl' ] },
            { name: 'styles', items: [ 'Styles', 'Format', 'Font', 'FontSize' ] },
            { name: 'colors', items: [ 'TextColor', 'BGColor' ] },
            { name: 'tools', items: [ 'UIColor', 'Maximize', 'ShowBlocks' ] }";
                    hasImageToolbarButton = true;
                    break;
                case CkEditorToolbar.AllOnOneRow:
                    toolbarSettings =
                        @"            { name: 'document',    groups: [ 'mode', 'document', 'doctools' ], items: [ 'Source', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', 'Templates', 'document' ] },
            { name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ], items: [ 'Find', 'Replace', 'SelectAll', 'Scayt' ] },
            { name: 'insert', items: [ 'CreatePlaceholder', 'Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe', 'InsertPre' ] },
            { name: 'forms', items: [ 'Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField' ] },
            { name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] },
            { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ], items: [ 'Bold', 'Italic', 'Underline', 'Subscript', 'Superscript', 'RemoveFormat' ] },
            { name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align' ], items: [ 'NumberedList', 'BulletedList', 'Outdent', 'Indent', 'CreateDiv', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'BidiLtr', 'BidiRtl' ] },
            { name: 'styles', items: [ 'Styles', 'Format', 'Font', 'FontSize' ] },
            { name: 'colors', items: [ 'TextColor', 'BGColor' ] },
            { name: 'tools', items: [ 'UIColor', 'Maximize', 'ShowBlocks' ] }";
                    hasImageToolbarButton = true;
                    break;
                case CkEditorToolbar.AllOnOneRowNoMaximize:
                    toolbarSettings =
                        @"            { name: 'document',    groups: [ 'mode', 'document', 'doctools' ], items: [ 'Source', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', 'Templates', 'document' ] },
            { name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ], items: [ 'Find', 'Replace', 'SelectAll', 'Scayt' ] },
            { name: 'insert', items: [ 'CreatePlaceholder', 'Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe', 'InsertPre' ] },
            { name: 'forms', items: [ 'Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField' ] },
            { name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] },
            { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ], items: [ 'Bold', 'Italic', 'Underline', 'Subscript', 'Superscript', 'RemoveFormat' ] },
            { name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align' ], items: [ 'NumberedList', 'BulletedList', 'Outdent', 'Indent', 'CreateDiv', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'BidiLtr', 'BidiRtl' ] },
            { name: 'styles', items: [ 'Styles', 'Format', 'Font', 'FontSize' ] },
            { name: 'colors', items: [ 'TextColor', 'BGColor' ] },
            { name: 'tools', items: [ 'UIColor', 'ShowBlocks' ] }";
                    hasImageToolbarButton = true;
                    break;
                case CkEditorToolbar.Minimal:
                    toolbarSettings = @"            { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ], items: [ 'Bold', 'Italic', 'Underline', 'Subscript', 'Superscript', 'RemoveFormat' ] },
            { name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align' ], items: [ 'NumberedList', 'BulletedList', 'Outdent', 'Indent', 'CreateDiv', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'BidiLtr', 'BidiRtl' ] },
            { name: 'styles', items: [ 'Styles', 'Format', 'Font', 'FontSize' ] },
            { name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] }";
                    hasImageToolbarButton = false;
                    break;
                case CkEditorToolbar.MinimalWithImages:
                    toolbarSettings = @"            { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ], items: [ 'Bold', 'Italic', 'Underline', 'Subscript', 'Superscript', 'RemoveFormat' ] },
            { name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align' ], items: [ 'NumberedList', 'BulletedList', 'Outdent', 'Indent', 'CreateDiv', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'BidiLtr', 'BidiRtl' ] },
            { name: 'insert', items: [ 'Image', 'Table', 'HorizontalRule', 'SpecialChar' ] },
            { name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] }";
                    hasImageToolbarButton = true;
                    break;
                case CkEditorToolbar.None:
                    toolbarSettings = String.Empty;
                    hasImageToolbarButton = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("ckEditorToolbarMode");
            }
            return new CkEditorToolbarJavascript(toolbarSettings, hasImageToolbarButton);
        }
    }
}
