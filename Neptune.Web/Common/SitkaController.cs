/*-----------------------------------------------------------------------
<copyright file="SitkaController.cs" company="Sitka Technology Group">
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

using System.Collections.ObjectModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Neptune.Common.DesignByContract;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Common
{
    public abstract partial class SitkaController : Controller
    {
        public const string CustomErrorMessageDivID = "customErrorMessageDiv";
        public const string CustomErrorMessageReplaceToken = "CUSTOM ERROR HERE";
        public const string CustomNotFoundMessageReplaceToken = "CUSTOM NOT FOUND HERE";

        private ICompositeViewEngine _viewEngine;
        protected SitkaController()
        {
        }

        protected SitkaController(ICompositeViewEngine viewEngine)
        {
            _viewEngine = viewEngine;
        }

        public const string StatusErrorIndex = "StatusError";
        public const string StatusMessageIndex = "StatusMessage";
        public const string InfoMessageIndex = "InfoMessage";

        public const string NotAuthorizedLoginText = "You do not have permission to do that, please log in.";

        protected static ReadOnlyCollection<MethodInfo> AllControllerActionMethodsProtected;

        private const string ControllerSuffix = "Controller";
        private const string ViewsRootNamespace = "Views";

        public static void SetErrorForDisplay(TempDataDictionary tempData, string errorMessage)
        {
            SetMessage(StatusErrorIndex, errorMessage, tempData);
        }

        protected void SetErrorForDisplay(string errorMessage)
        {
            SetMessage(StatusErrorIndex, errorMessage, TempData);
        }

        protected void ClearErrorForDisplay()
        {
            RemoveMessage(StatusErrorIndex, TempData);
        }

        protected void SetMessageForDisplay(string message)
        {
            SetMessage(StatusMessageIndex, message, TempData);
        }

        protected void ClearMessageForDisplay()
        {
            RemoveMessage(StatusMessageIndex, TempData);
        }

        private static void RemoveMessage(string index, ITempDataDictionary tempData)
        {
            tempData.Remove(index);
        }

        private static void SetMessage(string index, string message, ITempDataDictionary tempData)
        {
            tempData[index] = message;
        }

        protected void SetInfoForDisplay(string message)
        {
            SetMessage(InfoMessageIndex, message, TempData);
        }

        protected void ClearInfoForDisplay()
        {
            RemoveMessage(InfoMessageIndex, TempData);
        }

        protected void RequireViewPageIsInControllerViewDirectory<TView>()
        {
            var thisControllerType = GetType();
            var viewType = typeof(TView);

            var viewName = viewType.FullName;
            var thisControllerTypeName = thisControllerType.Name;

            // Is this one of several exceptions to the rule? If so, they don't need to have the same scope.
            var viewNamespace = (viewType.Namespace ?? String.Empty);
            var viewNamespaceSet = viewNamespace.Split('.');

            Check.Require(viewNamespaceSet.Contains(ViewsRootNamespace),
                $"Could not find views root namespace {ViewsRootNamespace} in the view {viewName}, is it a proper view?");

            var isViewShared = viewNamespaceSet.Contains("Shared");

            // Otherwise, View and Controller should be in same directory.
            var viewNameLastNamespace = viewNamespaceSet.Last();
            var thisControllerName = ControllerTypeToControllerName(thisControllerType);
            var controllerAndPageInSameDirectory = (viewNameLastNamespace == thisControllerName);

            Check.Require(isViewShared || controllerAndPageInSameDirectory,
                $"You can't cross boundaries on controllers ({thisControllerTypeName} -> {viewName}). You must return a View that is within your controller's view directory or a shared one, otherwise view resolution will fail.");
        }

        /// <summary>
        /// Convert a type into a controller name following MVC conventions of removing the controller suffix <see cref="ControllerSuffix" /> type: FooController => string: Foo
        /// </summary>
        /// <param name="controllerType">Type of the controller</param>
        /// <returns>string of the name minus the suffix</returns>
        public static string ControllerTypeToControllerName(Type controllerType)
        {
            var typeName = controllerType.Name;
            Check.Require(typeName.EndsWith(ControllerSuffix),
                $"Type {typeName} does not match naming convention for controller, expected suffix \"{ControllerSuffix}\"");
            var controllerTypeToControllerName = typeName.Substring(0, typeName.Length - ControllerSuffix.Length);
            Check.RequireNotNullNotEmptyNotWhitespace(controllerTypeToControllerName,
                $"Got an empty controller name off of type {typeName} after removing suffix \"{ControllerSuffix}\", illegal name unreachable by routes");
            return controllerTypeToControllerName;
        }

        /// <summary>
        /// Finds the name of the MVC area for a given controller type
        /// </summary>
        /// <param name="controllerType">Type of the controller</param>
        /// <returns>string of the area</returns>
        public static string ControllerTypeToAreaName(Type controllerType)
        {
            var namespaceParts = new Stack<string>(controllerType.FullName.Split('.'));

            var typeName = namespaceParts.Pop();
            Check.Require(namespaceParts.Pop() == "Controllers",
                $"Folder containing type {typeName} does not match expected name convention of \"Controllers\"");

            var areaName = namespaceParts.Pop();
            var areasFolder = namespaceParts.Pop();

            return areasFolder.ToLower() != "areas" ? string.Empty : areaName;
        }

        /// <summary>
        /// Convert a type into a controller name for the URL following MVC conventions of removing the controller suffix <see cref="ControllerSuffix" /> type: FooController => string: Foo.mvc or Foo
        /// </summary>
        /// <param name="controllerType">Type of the controller</param>
        /// <returns>string of the name minus the suffix plus optional .mvc file extension</returns>
        public static string ControllerTypeToControllerNameForUrl(Type controllerType)
        {
            var controllerName = ControllerTypeToControllerName(controllerType);
            return ControllerNameToUrlSegment(controllerName);
        }

        /// <summary>
        /// Convert a controller name into the segment of the URL following MVC conventions adding .mvc as needed: Foo => string: Foo.mvc or Foo
        /// </summary>
        /// <param name="controllerName">Controller name (name minus the "Controller" suffix)</param>
        public static string ControllerNameToUrlSegment(string controllerName)
        {
            return controllerName;
        }

        public static List<MethodInfo> FindAttributedMethods(Type webServiceType, Type attributeType)
        {
            var methods = webServiceType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            return methods.Where(m => m.GetCustomAttributes(true).Any(a => a.GetType() == attributeType)).ToList();
        }

        protected ActionResult RedirectToReferrerOrTo<T>(SitkaRoute<T> fallbackRoute)
            where T : Controller
        {
            var referrer = HttpContext.Request.GetTypedHeaders().Referer;
            if (referrer != null)
            {
                return Redirect(referrer.AbsoluteUri);
            }

            return RedirectToAction(fallbackRoute);
        }

        protected RedirectResult RedirectToAction<T>(SitkaRoute<T> route) where T : Controller
        {
            return RedirectToActionStatic(route);
        }

        protected ActionResult RedirectToActionWithError<T>(SitkaRoute<T> route, string error) where T : Controller
        {
            SetErrorForDisplay(error);
            return RedirectToActionStatic(route);
        }

        public static RedirectResult RedirectToActionStatic<T>(SitkaRoute<T> route) where T : Controller
        {
            return new RedirectResult(route.BuildUrlFromExpression());
        }

        /// <summary>
        /// Preferred signature for creating view results
        /// </summary>
        protected ViewResult RazorView<TPage, TViewData>(TViewData viewData) where TPage : TypedWebViewPage<TViewData>
        {
            SetViewDataTyped(viewData, ViewData);
            return ViewImpl<TPage>(null);
        }

        private static void SetViewDataTyped<TViewData>(TViewData viewData, ViewDataDictionary viewDataDictionary)
        {
            // ReSharper disable CompareNonConstrainedGenericWithNull
            if (!viewData.GetType().IsValueType && viewData == null)
            {
                throw new ArgumentNullException("viewData");
            }
            // ReSharper restore CompareNonConstrainedGenericWithNull
            viewDataDictionary[TypedWebViewPage.ViewDataDictionaryKey] = viewData;
        }

        protected virtual ViewResult ViewImpl<TPage>(object viewData)
        {
            RequireViewPageIsInControllerViewDirectory<TPage>();
            var viewName = typeof(TPage).Name;

#pragma warning disable 612,618
            return base.View(viewName, viewData);
#pragma warning restore 612,618
        }

        private static void EnsureViewModelIsSet<TViewModel>(TViewModel viewModel)
        {
            // ReSharper disable CompareNonConstrainedGenericWithNull
            if (!viewModel.GetType().IsValueType && viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
            // ReSharper restore CompareNonConstrainedGenericWithNull
        }

        /// <summary>
        /// Preferred signature for creating view results
        /// </summary>
        protected ViewResult RazorView<TPage, TViewData, TViewModel>(TViewData viewData, TViewModel viewModel) where TPage : TypedWebViewPage<TViewData, TViewModel>
        {
            EnsureViewModelIsSet(viewModel);
            SetViewDataTyped(viewData, ViewData);
            return ViewImpl<TPage>(viewModel);
        }

        protected PartialViewResult RazorPartialView<TPage, TViewData>(TViewData viewData) where TPage : TypedWebPartialViewPage<TViewData>
        {
            SetViewDataTyped(viewData, ViewData);
            return PartialViewImpl<TPage>(null);
        }

        protected PartialViewResult RazorPartialView<TPage, TViewData, TViewModel>(TViewData viewData, TViewModel viewModel) where TPage : TypedWebPartialViewPage<TViewData, TViewModel>
        {
            EnsureViewModelIsSet(viewModel);
            SetViewDataTyped(viewData, ViewData);
            return PartialViewImpl<TPage>(viewModel);
        }

        protected virtual PartialViewResult PartialViewImpl<TPage>(object viewModel)
        {
            RequireViewPageIsInControllerViewDirectory<TPage>();
            var viewName = typeof(TPage).Name;

#pragma warning disable 612,618
            return base.PartialView(viewName, viewModel);
#pragma warning restore 612,618
        }


        protected async Task<string> RenderPartialViewToString(string viewName, object viewData)
        {
            return await RenderPartialViewToString(viewName, viewData, viewData);
        }

        protected async Task<string> RenderPartialViewToString(string viewName, object viewData, object viewModel)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData[TypedWebViewPage.ViewDataDictionaryKey] = viewData;

            ViewData.Model = viewModel;

            await using var sw = new StringWriter();
            var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
            var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw,
                new HtmlHelperOptions());
            await viewResult.View.RenderAsync(viewContext);

            return sw.GetStringBuilder().ToString();
        }
    }
}
