using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Allows Viewing an OVTA")]
    public class OnlandVisualTrashAssessmentDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<OnlandVisualTrashAssessment>
    {
        private readonly NeptuneFeatureWithContextImpl<OnlandVisualTrashAssessment> _lakeTahoeInfoFeatureWithContextImpl;

        public OnlandVisualTrashAssessmentDeleteFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<OnlandVisualTrashAssessment>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, OnlandVisualTrashAssessment contextModelObject, NeptuneDbContext dbContext)
        {
            return new OnlandVisualTrashAssessmentEditFeature().HasPermission(person, contextModelObject, dbContext);
        }
    }
}