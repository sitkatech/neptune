using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows creating a Field Visit for a BMP if you are assigned to edit that BMP's jurisdiction")]
    public class FieldVisitCreateFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<TreatmentBMP>
    {
        private readonly NeptuneFeatureWithContextImpl<TreatmentBMP> _lakeTahoeInfoFeatureWithContextImpl;

        public FieldVisitCreateFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<TreatmentBMP>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMP contextModelObject, NeptuneDbContext dbContext)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDForFeatureContextCheck(dbContext, contextModelObject.TreatmentBMPID);
            return HasPermission(person, treatmentBMP);
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMP treatmentBMP)
        {
            var canManageStormwaterJurisdiction =
                person.IsAssignedToStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            if (!canManageStormwaterJurisdiction)
            {
                return new PermissionCheckResult(
                    $"You aren't assigned to manage Treatment BMPs for Jurisdiction {treatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}