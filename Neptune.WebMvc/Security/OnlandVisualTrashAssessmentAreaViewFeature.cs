using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Allows Viewing an OVTA Area")]
    public class OnlandVisualTrashAssessmentAreaViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<OnlandVisualTrashAssessmentArea>
    {
        private readonly NeptuneFeatureWithContextImpl<OnlandVisualTrashAssessmentArea> _lakeTahoeInfoFeatureWithContextImpl;

        public OnlandVisualTrashAssessmentAreaViewFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<OnlandVisualTrashAssessmentArea>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, OnlandVisualTrashAssessmentArea contextModelObject, NeptuneDbContext dbContext)
        {
            return HasPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, OnlandVisualTrashAssessmentArea contextModelObject)
        {
            if (person.IsAnonymousOrUnassigned() || person.IsAdministrator())
            {
                return new PermissionCheckResult();
            }

            if (person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdictionID))
            {
                return new PermissionCheckResult();
            }

            return new PermissionCheckResult(
                $"You do not have permission to view or manage OVTA Areas for {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
        }
    }
}