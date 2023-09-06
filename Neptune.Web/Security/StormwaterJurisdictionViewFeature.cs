using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing an Stormwater Jurisdiction")]
    public class StormwaterJurisdictionViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<StormwaterJurisdiction>
    {
        private readonly NeptuneFeatureWithContextImpl<StormwaterJurisdiction> _lakeTahoeInfoFeatureWithContextImpl;

        public StormwaterJurisdictionViewFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<StormwaterJurisdiction>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, StormwaterJurisdiction contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, StormwaterJurisdiction contextModelObject)
        {
            if (person.IsAnonymousOrUnassigned() || person.IsAdministrator())
            {
                return new PermissionCheckResult();
            }

            if (person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdictionID))
            {
                return new PermissionCheckResult();
            }

            return new PermissionCheckResult($"You do not have permission to view or manage Stormwater Jurisdiction {contextModelObject.GetOrganizationDisplayName()}");
        }
    }
}