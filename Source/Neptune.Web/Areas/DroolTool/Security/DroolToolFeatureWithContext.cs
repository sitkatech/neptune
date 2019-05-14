using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.DroolTool.Security
{
    public abstract class DroolToolFeatureWithContext : NeptuneBaseFeature, IActionFilter
    {
        public IActionFilter ActionFilter;

        protected DroolToolFeatureWithContext(IEnumerable<Role> grantedRoles) : base(grantedRoles.Select(x => (IRole)x).ToList(), NeptuneArea.DroolTool)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ActionFilter.OnActionExecuting(filterContext);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ActionFilter.OnActionExecuted(filterContext);
        }
    }
}