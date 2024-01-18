/*-----------------------------------------------------------------------
<copyright file="TinyMCEExtension.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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
using System.Text.Encodings.Web;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Neptune.WebMvc.Common.Models;
using static System.Net.Mime.MediaTypeNames;
using TagBuilder = Microsoft.AspNetCore.Mvc.Rendering.TagBuilder;

namespace Neptune.WebMvc.Common
{
    public static class TinyMCEExtension
    {
        public enum TinyMCEToolbarStyle
        {
            All,
            AllOnOneRow,
            AllOnOneRowNoMaximize,
            Minimal,
            MinimalWithImages,
            None
        }

        public static IHtmlContent TinyMCEEditorFor<TModel, TValue>(this IHtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression,
            TinyMCEToolbarStyle tinyMceToolbarStyleMode,
            bool editable,
            int uniqueID) where TModel : FormViewModel
        {
            return TinyMCEEditorFor(helper, expression, tinyMceToolbarStyleMode, editable, uniqueID, 200);
        }

        public static IHtmlContent TinyMCEEditorFor<TModel, TValue>(this IHtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression,
            TinyMCEToolbarStyle tinyMceToolbarStyleMode,
            bool editable,
            int? uniqueID,
            int? height) where TModel : FormViewModel
        {
            var expressionProvider = new ModelExpressionProvider(helper.MetadataProvider);
            var metadata = expressionProvider.CreateModelExpression(helper.ViewData, expression);
            var modelValue = (string)metadata.Model;

            if (!editable)
            {
                return new HtmlString(modelValue);
            }

            var modelID = helper.IdFor(expression);

            var textAreaID = $"TinyMCEEditorFor{modelID}{(uniqueID.HasValue ? $"ID{uniqueID.Value}" : string.Empty)}";

            var htmlAttributes = new Dictionary<string, object>() { { "id", textAreaID }, { "contentEditable", "true" }, { "data-editor-id", textAreaID } };

            var generateJavascript = GenerateJavascript(textAreaID, tinyMceToolbarStyleMode, height);
            var textAreaHtmlString = helper.TextAreaFor(expression, htmlAttributes);

            var writer = new StringWriter();
            var builder = new HtmlContentBuilder();
            builder.AppendFormat("{0}{1}", textAreaHtmlString, generateJavascript);
            builder.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        public static IHtmlContent GenerateJavascript(string textAreaID, TinyMCEToolbarStyle tinyMceToolbarStyleMode, int? height)
        {
            var tag = new TagBuilder("script");
            tag.Attributes.Add("type", "text/javascript");
            tag.Attributes.Add("language", "javascript");

            var tinyMCEEditorToolbarJavascript = GenerateToolbarSettings(tinyMceToolbarStyleMode);

            var wireUpJsForImageUploader = string.Empty;
            if (tinyMCEEditorToolbarJavascript.HasImageToolbarButton)
            {
                wireUpJsForImageUploader = @"file_picker_callback: (cb, value, meta) => {
                              const input = document.createElement(""input"")
                              input.setAttribute(""type"", ""file"")
                              input.setAttribute(""accept"", ""image/*"")
                              input.addEventListener(""change"", e => {
                                const file = e.target.files[0];
                                const reader = new FileReader();
                                reader.addEventListener(""load"", () => {
                                  const id = ""blobid"" + new Date().getTime();
                                  const blobCache = tinymce.activeEditor.editorUpload.blobCache;
                                  const base64 = reader.result.split("","")[1];
                                  const blobInfo = blobCache.create(id, file, base64);
                                  blobCache.add(blobInfo);
                                  /* call the callback and populate the Title field with the file name */
                                  cb(blobInfo.blobUri(), { title: file.name });
                                });
                                reader.readAsDataURL(file)
                              })
                              input.click()
                            },";
            }

            tag.InnerHtml.AppendHtml(string.Format(@"
                // <![CDATA[
                jQuery(document).ready(function ()
                {{
                   tinymce.init({{
                            selector: '#{0}',
                            height: {5},
                            menubar: false,
                            toolbar: '{1}',
                            entity_encoding: 'named+numeric',
                            plugins: '{2}',
                            toolbar_mode: '{4}',
                            browser_spellcheck: true,
                            file_picker_types: 'image',
                            images_file_types: 'jpg,svg,webp,gif',
                            image_title: true,
                            {3}
                            setup: function (editor) {{
                                editor.on('FullscreenStateChanged', function (e) {{
                                    if (e.state) {{
                                        $('.modal-dialog').attr('style', 'transform: none !important');
                                    }} else {{
                                        $('.modal-dialog').attr('style', 'transform: translate(0,0)');
                                    }}
                                }});
                            }}
                    }});
                }});
                // ]]>
            ", textAreaID, tinyMCEEditorToolbarJavascript.JavascriptForToolbar, tinyMCEEditorToolbarJavascript.Plugins, wireUpJsForImageUploader, tinyMCEEditorToolbarJavascript.ToolbarMode, height ?? 200));

            var writer = new StringWriter();
            var builder = new HtmlContentBuilder();

            builder.AppendFormat("{0}", tag);
            builder.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        private class TinyMCEEditorToolbarJavascript
        {
            public readonly string JavascriptForToolbar;
            public readonly bool HasImageToolbarButton;
            public readonly string Plugins;
            public readonly string ToolbarMode;

            public TinyMCEEditorToolbarJavascript(string javascriptForToolbar, bool hasImageToolbarButton, string plugins, string toolbarMode)
            {
                JavascriptForToolbar = javascriptForToolbar;
                HasImageToolbarButton = hasImageToolbarButton;
                Plugins = plugins;
                ToolbarMode = toolbarMode;

            }
        }

        private static TinyMCEEditorToolbarJavascript GenerateToolbarSettings(TinyMCEToolbarStyle tinyMceToolbarStyleMode)
        {
            bool hasImageToolbarButton;
            string toolbarSettings;
            string plugins;
            string toolbarMode;
            switch (tinyMceToolbarStyleMode)
            {
                case TinyMCEToolbarStyle.All:
                    plugins = "code lists link image table code help wordcount charmap anchor fullscreen";
                    toolbarSettings =
                        "undo redo | styleselect | bold italic | bullist numlist | alignleft aligncenter alignright alignjustify | outdent indent | blockquote table | image media link unlink | styles | code | fullscreen ";
                    toolbarMode = "wrap";
                    hasImageToolbarButton = true;
                    break;
                case TinyMCEToolbarStyle.AllOnOneRow:
                    plugins = "AllOnOneRow";
                    toolbarMode = "floating";
                    toolbarSettings = 
                        "undo redo | styleselect | bold italic | bullist numlist | alignleft aligncenter alignright alignjustify | outdent indent | blockquote table | image media link unlink | styles | code";
                    hasImageToolbarButton = true;
                    break;
                case TinyMCEToolbarStyle.AllOnOneRowNoMaximize:
                    plugins = "lists link image table code help wordcount charmap anchor";
                    toolbarMode = "floating";
                    toolbarSettings =
                        "undo redo | styleselect | bold italic | bullist numlist | alignleft aligncenter alignright alignjustify | outdent indent | blockquote table | image media link unlink | styles | code";
                    hasImageToolbarButton = true;
                    break;
                case TinyMCEToolbarStyle.Minimal:
                    toolbarSettings =
                        "undo redo | styleselect | bold italic | bullist numlist | alignleft aligncenter alignright alignjustify | outdent indent | blockquote table | image media link unlink | styles | code ";
                    plugins = "lists link code help wordcount anchor";
                    toolbarMode = "floating";
                    hasImageToolbarButton = false;
                    break;
                case TinyMCEToolbarStyle.MinimalWithImages:
                    toolbarSettings =
                        "undo redo | styleselect | bold italic | bullist numlist | alignleft aligncenter alignright alignjustify | outdent indent | blockquote table | image media link unlink | styles | code";
                    plugins = "lists link image table code help wordcount charmap anchor";
                    toolbarMode = "floating";
                    hasImageToolbarButton = true;
                    break;
                case TinyMCEToolbarStyle.None:
                    toolbarSettings = String.Empty;
                    hasImageToolbarButton = false;
                    toolbarMode = "floating";
                    plugins = string.Empty;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("tinyMceToolbarStyleMode");
            }
            return new TinyMCEEditorToolbarJavascript(toolbarSettings, hasImageToolbarButton, plugins, toolbarMode);
        }
    }
}