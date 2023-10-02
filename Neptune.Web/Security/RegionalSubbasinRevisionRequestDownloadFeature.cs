using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Requires JurisdictionManager role")]
    public class RegionalSubbasinRevisionRequestDownloadFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<RegionalSubbasinRevisionRequest>
    {
        private readonly NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest> _lakeTahoeInfoFeatureWithContextImpl;

        public RegionalSubbasinRevisionRequestDownloadFeature() : base(new List<RoleEnum> { RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.JurisdictionManager })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, RegionalSubbasinRevisionRequest contextModelObject, NeptuneDbContext dbContext)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDForFeatureContextCheck(dbContext, contextModelObject.TreatmentBMPID);
            if (person.IsAdministrator() ||
                (person.RoleID == Role.JurisdictionManager.RoleID && person.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdictionID).Contains(treatmentBMP.StormwaterJurisdictionID)))
            {
                return new PermissionCheckResult();
            }
            
            return new PermissionCheckResult("You do not have permission to download the Regional Subbasin Revision Request geometry.");
        }
    }
}
