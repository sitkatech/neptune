using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Allows deleting a Delineation if you are assigned to manage its BMP's jurisdiction")]
    public class DelineationDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<Delineation>
    {
        private readonly NeptuneFeatureWithContextImpl<Delineation> _lakeTahoeInfoFeatureWithContextImpl;

        public DelineationDeleteFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<Delineation>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, Delineation contextModelObject, NeptuneDbContext dbContext)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDForFeatureContextCheck(dbContext, contextModelObject.TreatmentBMPID);
            return HasPermission(person, treatmentBMP);
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMP treatmentBMP)
        {
            var organizationDisplayName = treatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName();
            var canManageStormwaterJurisdiction =
                person.IsAssignedToStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            if (!canManageStormwaterJurisdiction)
            {
                return new PermissionCheckResult(
                    $"You aren't assigned to manage Treatment BMPs for Jurisdiction {organizationDisplayName}");
            }

            if (!(person.IsAdministrator() || person.Role == Role.JurisdictionManager))
            {
                return new PermissionCheckResult(
                    $"You do not have permission to delete Treatment BMP delineation for Jurisdiction {organizationDisplayName}");
            }

            return new PermissionCheckResult();
        }
    }
}