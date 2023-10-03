using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing a Treatment BMP")]
    public class RegionalSubbasinRevisionRequestViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<RegionalSubbasinRevisionRequest>
    {
        private readonly NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest> _lakeTahoeInfoFeatureWithContextImpl;

        public RegionalSubbasinRevisionRequestViewFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, RegionalSubbasinRevisionRequest contextModelObject, NeptuneDbContext dbContext)
        {
            // they just need permission for the BMP in question...
            var treatmentBMP = TreatmentBMPs.GetByIDForFeatureContextCheck(dbContext, contextModelObject.TreatmentBMPID);
            return new TreatmentBMPEditFeature().HasPermission(person, treatmentBMP);
        }
    }
}
