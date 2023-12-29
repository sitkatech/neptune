using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription(
        "Allows verifying and unverifying Field Visits for a BMP if you are assigned to manage that BMP's jurisdiction")]
    public class FieldVisitVerifyFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<EFModels.Entities.FieldVisit>
    {
        private readonly NeptuneFeatureWithContextImpl<EFModels.Entities.FieldVisit> _lakeTahoeInfoFeatureWithContextImpl;

        public FieldVisitVerifyFeature()
            : base(new List<RoleEnum> {RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<EFModels.Entities.FieldVisit>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, FieldVisit contextModelObject,
            NeptuneDbContext dbContext)
        {
            var treatmentBMP =
                TreatmentBMPs.GetByIDForFeatureContextCheck(dbContext, contextModelObject.TreatmentBMPID);
            var isAssignedToStormwaterJurisdiction = person.IsAssignedToStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            if (!isAssignedToStormwaterJurisdiction)
            {
                return new PermissionCheckResult(
                    $"You aren't assigned to edit Field Visit data for Jurisdiction {treatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}