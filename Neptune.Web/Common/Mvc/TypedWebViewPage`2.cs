/*-----------------------------------------------------------------------
<copyright file="TypedWebViewPage`2.cs" company="Sitka Technology Group">
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

using JetBrains.Annotations;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Neptune.Web.Common.Mvc
{
    public abstract class TypedWebViewPage<TViewData> : TypedWebViewPage<TViewData, object>
    {

    }

    public abstract class TypedWebPartialViewPage<TViewData> : TypedWebPartialViewPage<TViewData, object>
    {

    }

    public static class TypedWebViewPage
    {
        public const string ViewDataDictionaryKey = "ViewDataObject";
        public const string LayoutDictionaryKey = "LayoutObject";
    }

    public abstract class TypedWebViewPage<TViewData, TViewModel> : RazorPage<TViewModel>
    {
        static TypedWebViewPage()
        {
            if (typeof(TViewData) == typeof(object))
            {
                throw new InvalidOperationException("You must explicitly specify TViewData when creating a typed view. Use an @model declaration to set the view-model type. Note that TViewData cannot be object or dynamic.");
            }
        }

        private object ViewDataViaViewDataDictionary => base.ViewData[TypedWebViewPage.ViewDataDictionaryKey];

        [Obsolete("Don't use this")]
        public new ViewDataDictionary ViewData
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        [NotNull]
        public TViewData ViewDataTyped
        {
            get
            {
                if (ViewDataViaViewDataDictionary == null)
                {
                    throw new InvalidOperationException("You must specify ViewData when rendering a typed view. Derive your controllers from TypedController and use an overload of View() that accepts a viewModel parameter.");
                }

                if (!(ViewDataViaViewDataDictionary is TViewData))
                {
                    throw new InvalidOperationException($"The specified ViewData value is of type '{ViewDataViaViewDataDictionary.GetType()}' and must be of type '{typeof(TViewData)}'.");
                }
                return (TViewData)ViewDataViaViewDataDictionary;
            }
        }

        public HtmlString SetLayout([PathReference] string layout, [AspMvcModelType, JetBrains.Annotations.NotNull] object layoutModel)
        {
            if (layoutModel == null)
            {
                throw new ArgumentNullException("layoutModel");
            }
            base.ViewData[TypedWebViewPage.LayoutDictionaryKey] = layoutModel;
            base.Layout = layout;

            return null;
        }

        [Obsolete("You cannot use the Layout property when rendering a typed view. Use the SetLayout() method instead.", true)]
        public new string Layout
        {
            get
            {
                throw new InvalidOperationException("You cannot use the Layout property when rendering a typed view. Use the SetLayout() method instead.");
            }
            set
            {
                throw new InvalidOperationException("You cannot use the Layout property when rendering a typed view. Use the SetLayout() method instead.");
            }
        }
    }


    public abstract class TypedWebPartialViewPage<TViewData, TViewModel> : RazorPage<TViewModel>
    {
        static TypedWebPartialViewPage()
        {
            if (typeof(TViewData) == typeof(object))
            {
                throw new InvalidOperationException("You must explicitly specify TViewData when creating a typed view. Use an @model declaration to set the view-model type. Note that TViewData cannot be object or dynamic.");
            }
        }

        private object ViewDataViaViewDataDictionary => base.ViewData[TypedWebViewPage.ViewDataDictionaryKey];

        [Obsolete("Don't use this")]
        public new ViewDataDictionary ViewData
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        [NotNull]
        public TViewData ViewDataTyped
        {
            get
            {
                if (ViewDataViaViewDataDictionary == null)
                {
                    throw new InvalidOperationException("You must specify ViewData when rendering a typed view. Derive your controllers from TypedController and use an overload of View() that accepts a viewModel parameter.");
                }

                if (!(ViewDataViaViewDataDictionary is TViewData))
                {
                    throw new InvalidOperationException($"The specified ViewData value is of type '{ViewDataViaViewDataDictionary.GetType()}' and must be of type '{typeof(TViewData)}'.");
                }
                return (TViewData)ViewDataViaViewDataDictionary;
            }
        }

        public HtmlString SetLayout([PathReference] string layout, [AspMvcModelType] object layoutModel)
        {
            if (layoutModel == null)
            {
                throw new ArgumentNullException("layoutModel");
            }
            base.ViewData[TypedWebViewPage.LayoutDictionaryKey] = layoutModel;
            base.Layout = layout;

            return null;
        }

        [Obsolete("You cannot use the Layout property when rendering a typed view. Use the SetLayout() method instead.", true)]
        public new string Layout
        {
            get
            {
                throw new InvalidOperationException("You cannot use the Layout property when rendering a typed view. Use the SetLayout() method instead.");
            }
            set
            {
                throw new InvalidOperationException("You cannot use the Layout property when rendering a typed view. Use the SetLayout() method instead.");
            }
        }
    }
}
