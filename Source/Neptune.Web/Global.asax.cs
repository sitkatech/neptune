﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Neptune.Web.Common;
using FluentValidation.Mvc;
using LtInfo.Common;
using Neptune.Web.Controllers;
using log4net.Config;
using LtInfo.Common.LoggingFilters;
using LtInfo.Common.Mvc;
using SitkaController = Neptune.Web.Common.SitkaController;
using SitkaRouteTableEntry = Neptune.Web.Common.SitkaRouteTableEntry;

namespace Neptune.Web
{
    public class Global : SitkaGlobalBase
    {
        public static Dictionary<string, string> AreasDictionary = new Dictionary<string, string>
        {
            {string.Empty, NeptuneWebConfiguration.CanonicalHostName}
        };

        protected void Application_Start()
        {
            SitkaLogger.RegisterLogger(new NeptuneLogger());

            // this needs to match the Area Name declared in the Areas folder

            // create the default routes for the app and the areas
            var defaultRoutes =
                AreasDictionary.Select(
                    keyValuePair =>
                        new SitkaRouteTableEntry(string.Format("{0}_Default", keyValuePair.Key),
                            String.Empty,
                            string.Format("Neptune.Web{0}.Controllers", !string.IsNullOrWhiteSpace(keyValuePair.Key) ? string.Format(".Areas.{0}", keyValuePair.Key) : string.Empty),
                            SitkaController.DefaultController,
                            SitkaController.DefaultAction,
                            keyValuePair.Key,
                            keyValuePair.Value,
                            null,
                            false)).ToList();

            var viewLocations = new ViewEngineLocations { PartialViewLocations = new List<string>
            {
                "~/Views/Shared/TextControls/{0}.cshtml",
                "~/Views/Shared/ExpenditureAndBudgetControls/{0}.cshtml",
                "~/Views/Shared/PerformanceMeasureControls/{0}.cshtml",
                "~/Views/Shared/ProjectControls/{0}.cshtml",
                "~/Views/Shared/JurisdictionControls/{0}.cshtml",
                "~/Views/Shared/ProjectWatershedControls/{0}.cshtml",
                "~/Views/Shared/ProjectUpdateDiffControls/{0}.cshtml",
                "~/Views/Shared/EditAttributes/{0}.cshtml",
                "~/Views/Shared/SortOrder/{0}.cshtml",
                "~/Views/Shared/Location/{0}.cshtml",
                "~/Views/FieldVisit/ObservationTypePreview/{0}.cshtml",

            } };
            // read the log4net configuration from the web.config file
            XmlConfigurator.Configure();

            Logger.InfoFormat("Application Start{0}{1} version: {2}{0}Compiled: {3:MM/dd/yyyy HH:mm:ss}{0}"
                , Environment.NewLine
                , "Neptune"
                , SitkaWebConfiguration.WebApplicationVersionInfo.Value.ApplicationVersion
                , SitkaWebConfiguration.WebApplicationVersionInfo.Value.DateCompiled
            );

            RouteTableBuilder.Build(NeptuneBaseController.AllControllerActionMethods, defaultRoutes, AreasDictionary);
            SetupCustomViewLocationsForTemplates(viewLocations, AreasDictionary);
            ModelBinders.Binders.DefaultBinder = new SitkaDefaultModelBinder();

            RegisterGlobalFilters(GlobalFilters.Filters);
            FluentValidationModelValidatorProvider.Configure();
        }

        // ReSharper disable InconsistentNaming
        protected void Session_Start(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            // keep this seeming no-op line - it prevents "): Session state has created a session id, but cannot save it because the response was already flushed by the application." errors
#pragma warning disable 168
            var sessionID = Session.SessionID;
#pragma warning restore 168
        }

        // ReSharper disable InconsistentNaming
        protected void Application_Error(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            ApplicationError();
        }

        // ReSharper disable InconsistentNaming
        protected void Application_BeginRequest(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            // Call this in Application_BeginRequest because later on it can be too late to be mucking with the Response HTTP Headers
            AddCachingHeaders(Response, Request, SitkaWebConfiguration.CacheStaticContentTimeSpan);

            ApplicationBeginRequest();
            UnsupportedHttpMethodHandler.BeginRequestRespondToUnsupportedHttpMethodsWith405MethodNotAllowed(Request, Response);
            RedirectToCanonicalHostnameIfNeeded();
            Response.TrySkipIisCustomErrors = true;

        }

        /// <summary>
        /// If the URL doesn't match CanonicalHostName do a redirect. Otherwise do nothing. This is especially important for sites using SSL to get certificate to match up
        /// </summary>
        private void RedirectToCanonicalHostnameIfNeeded()
        {
            if (String.IsNullOrWhiteSpace(Request.Url.Host))
            {
                return;
            }
            var canonicalHostName = NeptuneWebConfiguration.GetCanonicalHost(Request.Url.Host, true);

            // Check for hostname match (deliberately case-insensitive, DNS is case-insensitive and so is SSL Cert for common name) against the canonical host name as specified in the configuration
            if (!String.Equals(Request.Url.Host, canonicalHostName, StringComparison.InvariantCultureIgnoreCase))
            {

                var builder = new UriBuilder(Request.Url) { Host = canonicalHostName };
                var newUri = builder.Uri;

                // Signal this as a permanent redirect 301 HTTP status not 302 since we'd want to update bad URLs
                Response.RedirectPermanent(newUri.AbsoluteUri);
            }
        }

        /// <summary>
        /// Try to log anything that's an error that's slipped past <see cref="Application_Error"/> such as certain errors from a webservice
        /// Like the <see cref="HttpStatusCode.UnsupportedMediaType"/> 415 which comes up if there's a SOAP binding mismatch
        /// </summary>
        // ReSharper disable InconsistentNaming
        protected void Application_EndRequest(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            ApplicationEndRequest();
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Require SSL from this point forward
            filters.Add(new RequireHttpsAttribute());
            filters.Add(new OpenIDNeptuneAuthorizeAttribute());
       }

        public override string ErrorUrl
        {
            get { return SitkaRoute<HomeController>.BuildAbsoluteUrlHttpsFromExpression(x => x.Error()); }
        }

        public override string NotFoundUrl
        {
            get { return SitkaRoute<HomeController>.BuildAbsoluteUrlHttpsFromExpression(x => x.NotFound()); }
        }

        public override string ErrorHtml
        {
            get { return "<h2>Aw Shucks!</h2><p>An error has occurred.   The development staff has been notified and will be working to fix this problem in a jiffy!</p>"; }
        }

        public override string NotFoundHtml
        {
            get { return "<h2>Aw Shucks!</h2><p>Sorry, the page or item you requested does not exist.</p>"; }
        }
    }
}