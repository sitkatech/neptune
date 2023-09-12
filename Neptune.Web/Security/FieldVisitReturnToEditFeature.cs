using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows returning unverified Vield Fisits to edit if you are assigned to edit that jurisdiction")]
    public class FieldVisitReturnToEditFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<EFModels.Entities.FieldVisit>
    {
        private readonly NeptuneFeatureWithContextImpl<EFModels.Entities.FieldVisit> _lakeTahoeInfoFeatureWithContextImpl;

        public FieldVisitReturnToEditFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<EFModels.Entities.FieldVisit>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, FieldVisit contextModelObject,
            NeptuneDbContext dbContext)
        {
            var isAssignedToStormwaterJurisdiction = person.IsAssignedToStormwaterJurisdiction(contextModelObject.TreatmentBMP.StormwaterJurisdictionID);

            if (!isAssignedToStormwaterJurisdiction)
            {
                return new PermissionCheckResult($"You aren't assigned to edit Field Visit data for Jurisdiction {contextModelObject.TreatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}