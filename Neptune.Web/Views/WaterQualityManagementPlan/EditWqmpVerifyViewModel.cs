using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Services;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpVerifyViewModel : NewWqmpVerifyViewModel
    {
        public bool DeleteStructuralDocumentFile { get; set; }

        public FileResource StructuralDocumentFileResource{ get; set; }


        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpVerifyViewModel()
        {
        }

        public EditWqmpVerifyViewModel(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPs, Person currentPerson) : base (waterQualityManagementPlanVerify.WaterQualityManagementPlan, waterQualityManagementPlanVerify, new List<QuickBMP>(), new List<EFModels.Entities.TreatmentBMP>())
        {

            StructuralDocumentFileResource = waterQualityManagementPlanVerify.FileResource;
            DeleteStructuralDocumentFile = false;

            WaterQualityManagementPlanVerifyQuickBMPSimples = waterQualityManagementPlanVerifyQuickBMPs.Select(x =>  x.AsSimpleDto()).OrderBy(x => x.QuickBMPName).ToList();
            WaterQualityManagementPlanVerifyTreatmentBMPSimples = waterQualityManagementPlanVerifyTreatmentBMPs.Select(x => x.AsSimpleDto()).OrderBy(x => x.TreatmentBMPName).ToList();

            
        }

        public void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, bool deleteStructuralDocumentFile, List<WaterQualityManagementPlanVerifyQuickBMPSimpleDto> waterQualityManagementPlanVerifyQuickBMPSimples, List<WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto> waterQualityManagementPlanVerifyTreatmentBMPSimples, Person currentPerson, NeptuneDbContext dbContext, FileResourceService fileResourceService)
        {
            base.UpdateModel(waterQualityManagementPlan,
                 waterQualityManagementPlanVerify, waterQualityManagementPlanVerifyQuickBMPSimples, waterQualityManagementPlanVerifyTreatmentBMPSimples, currentPerson, dbContext, fileResourceService);
        }
    }
}
