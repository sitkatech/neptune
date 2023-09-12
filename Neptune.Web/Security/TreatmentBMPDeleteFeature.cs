using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows deleting a Treatment BMP if you are assigned to manage that BMP's jurisdiction")]
    public class TreatmentBMPDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<TreatmentBMP>
    {
        private readonly NeptuneFeatureWithContextImpl<TreatmentBMP> _lakeTahoeInfoFeatureWithContextImpl;

        public TreatmentBMPDeleteFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<TreatmentBMP>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMP contextModelObject,
            NeptuneDbContext dbContext)
        {
            var canManageStormwaterJurisdiction = person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdictionID);
            if (!canManageStormwaterJurisdiction)
            {
                return new PermissionCheckResult($"You aren't assigned to manage Treatment BMPs for Jurisdiction {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            if (!(person.IsAdministrator() || person.Role == Role.JurisdictionManager))
            {
                return new PermissionCheckResult($"You do not have permission to delete Treatment BMPs for Jurisdiction {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}