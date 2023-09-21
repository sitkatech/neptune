using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Hippocamp.EFModels.Entities;

namespace Hippocamp.API.Services.Authorization
{
    public class LoggedInUnclassifiedFeature : AuthorizeAttribute, IAuthorizationFilter
    {
        public LoggedInUnclassifiedFeature() : base()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

        }
    }
}