using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Requires Jurisdiction Editor or Jurisdiction Manager role")]
    public class NeptuneViewAndRequiresJurisdictionsFeature : NeptuneFeature
    {
        public NeptuneViewAndRequiresJurisdictionsFeature()
            : base(new List<RoleEnum> { RoleEnum.Admin, RoleEnum.JurisdictionEditor, RoleEnum.JurisdictionManager, RoleEnum.SitkaAdmin })
        {
        }

        public override bool HasPermissionByPerson(Person? person)
        {
            if (base.HasPermissionByPerson(person))
            {
                if (person != null)
                {
                    if (person.IsAdministrator())
                    {
                        return true;
                    }

                    if (person.StormwaterJurisdictionPeople.Any())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    var redirectToLogin = new RedirectResult(NeptuneHelpers.GenerateLogInUrlWithReturnUrl());
        //    if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        filterContext.Result = redirectToLogin;
        //        return;
        //    }
        //    throw new SitkaRecordNotAuthorizedException($"You are not authorized for feature \"{FeatureName}\" or you are not assigned to any Jurisdictions. Log out and log in as a different user or request additional permissions.");
        //}
    }
}