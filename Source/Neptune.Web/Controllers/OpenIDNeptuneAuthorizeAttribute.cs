using System.Threading;
using System.Web;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Security.Shared;

namespace Neptune.Web.Controllers
{
    public class OpenIDNeptuneAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var principal = HttpRequestStorage.GetHttpContextUserThroughOwin();

            var attributeType = typeof(AnonymousUnclassifiedFeature);
            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(attributeType, true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(attributeType, true);

            if (!principal.Identity.IsAuthenticated && !skipAuthorization)
            {
                base.OnAuthorization(filterContext);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}