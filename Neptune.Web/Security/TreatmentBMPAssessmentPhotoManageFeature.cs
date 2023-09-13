using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows editing a Treatment BMP Assessment Photo if you are assigned to manage that BMP's jurisdiction")]
    public class TreatmentBMPAssessmentPhotoManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<TreatmentBMPAssessmentPhoto>
    {
        private readonly NeptuneFeatureWithContextImpl<TreatmentBMPAssessmentPhoto> _lakeTahoeInfoFeatureWithContextImpl;

        public TreatmentBMPAssessmentPhotoManageFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionEditor, RoleEnum.JurisdictionManager })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<TreatmentBMPAssessmentPhoto>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMPAssessmentPhoto contextModelObject,
            NeptuneDbContext dbContext)
        {
            var treatmentBMPAssessment = TreatmentBMPAssessments.GetByIDForFeatureContextCheck(dbContext, contextModelObject.TreatmentBMPAssessmentID);
            return HasPermission(person, treatmentBMPAssessment.TreatmentBMP);
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMP treatmentBMP)
        {
            return new TreatmentBMPManageFeature().HasPermission(person, treatmentBMP);
        }

    }
}
