using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing a Treatment BMP")]
    public class TreatmentBMPViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<TreatmentBMP>
    {
        private readonly NeptuneFeatureWithContextImpl<TreatmentBMP> _lakeTahoeInfoFeatureWithContextImpl;

        public TreatmentBMPViewFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<TreatmentBMP>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMP contextModelObject,
            NeptuneDbContext dbContext)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDForFeatureContextCheck(dbContext, contextModelObject.TreatmentBMPID);
            return HasPermission(person, treatmentBMP);
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMP treatmentBMP)
        {
            var organizationDisplayName = treatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName();
            if (person.IsAnonymousOrUnassigned() &&
                treatmentBMP.StormwaterJurisdiction.StormwaterJurisdictionPublicBMPVisibilityTypeID ==
                (int)StormwaterJurisdictionPublicBMPVisibilityTypeEnum.None)
            {
                return new PermissionCheckResult(
                    $"You don't have permission to view BMPs for Jurisdiction {organizationDisplayName}");
            }

            // verified BMPs are available for unassigned/anonymous users and therefore all users
            if (treatmentBMP.InventoryIsVerified)
            {
                return new PermissionCheckResult();
            }

            var isAssignedToTreatmentBMP = person.IsAssignedToStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            if (!isAssignedToTreatmentBMP)
            {
                return new PermissionCheckResult(
                    $"You don't have permission to view BMPs for Jurisdiction {organizationDisplayName}");
            }

            return new PermissionCheckResult();
        }
    }
}
