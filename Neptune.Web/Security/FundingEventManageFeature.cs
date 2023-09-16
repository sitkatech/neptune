using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows editing a Treatment BMP's Funding Events if you are assigned to manage that BMP's jurisdiction")]
    public class FundingEventManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<FundingEvent>
    {
        private readonly NeptuneFeatureWithContextImpl<FundingEvent> _lakeTahoeInfoFeatureWithContextImpl;

        public FundingEventManageFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<FundingEvent>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, FundingEvent contextModelObject,
            NeptuneDbContext dbContext)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDForFeatureContextCheck(dbContext, contextModelObject.TreatmentBMPID);
            return new TreatmentBMPManageFeature().HasPermission(person, treatmentBMP);
        }
    }
}