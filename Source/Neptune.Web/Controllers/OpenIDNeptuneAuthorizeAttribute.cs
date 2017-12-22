using System.Threading;
using System.Web;
using System.Web.Mvc;
using Neptune.Web.Security.Shared;

namespace Neptune.Web.Controllers
{
    public class OpenIDNeptuneAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var principal = filterContext.HttpContext.User;

            var attributeType = typeof(AnonymousUnclassifiedFeature);
            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(attributeType, true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(attributeType, true);

            if (principal.Identity.IsAuthenticated) // we have a token and we can determine the user.
            {
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
            }
            else
            {
                if (!skipAuthorization)
                {
                    base.OnAuthorization(filterContext);
                }

            }
        }

    }
}