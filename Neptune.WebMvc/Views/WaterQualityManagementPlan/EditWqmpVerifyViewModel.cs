using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
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

        public EditWqmpVerifyViewModel(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPs) : base (waterQualityManagementPlanVerify.WaterQualityManagementPlan, waterQualityManagementPlanVerify, new List<QuickBMP>(), new List<EFModels.Entities.TreatmentBMP>())
        {

            StructuralDocumentFileResource = waterQualityManagementPlanVerify.FileResource;
            DeleteStructuralDocumentFile = false;

            WaterQualityManagementPlanVerifyQuickBMPSimples = waterQualityManagementPlanVerifyQuickBMPs.Select(x => x.AsSimpleDto()).OrderBy(x => x.QuickBMPName).ToList();
            WaterQualityManagementPlanVerifyTreatmentBMPSimples = waterQualityManagementPlanVerifyTreatmentBMPs.Select(x => x.AsSimpleDto()).OrderBy(x => x.TreatmentBMPName).ToList();
        }
    }
}
