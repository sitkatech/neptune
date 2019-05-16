using Hangfire.Dashboard;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.ScheduledJobs
{
    public class HangfireNeptuneWebAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var person = HttpRequestStorage.Person;
            return person.IsAdministrator();
        }
    }
}
