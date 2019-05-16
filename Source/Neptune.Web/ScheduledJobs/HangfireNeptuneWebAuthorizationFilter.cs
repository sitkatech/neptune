using Hangfire.Dashboard;
using Keystone.Common.OpenID;
using Microsoft.Owin;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.ScheduledJobs
{
    public class HangfireNeptuneWebAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var owinContext = new OwinContext(context.GetOwinEnvironment());
            var person = KeystoneClaimsHelpers.GetOpenIDUserFromPrincipal(owinContext.Authentication.User,
                PersonModelExtensions.GetAnonymousSitkaUser(),
                HttpRequestStorage.DatabaseEntities.People.GetPersonByPersonGuid);
            return person.IsAdministrator();
        }
    }
}
