using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

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

        public EditWqmpVerifyViewModel(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPs, Person currentPerson) : base (waterQualityManagementPlanVerify.WaterQualityManagementPlan, waterQualityManagementPlanVerify, new List<QuickBMP>(), new List<Models.TreatmentBMP>())
        {

            StructuralDocumentFileResource = waterQualityManagementPlanVerify.FileResource;
            DeleteStructuralDocumentFile = false;

            WaterQualityManagementPlanVerifyQuickBMPSimples = waterQualityManagementPlanVerifyQuickBMPs.Select(x => new WaterQualityManagementPlanVerifyQuickBMPSimple(x)).OrderBy(x => x.QuickBMPName).ToList();
            WaterQualityManagementPlanVerifyTreatmentBMPSimples = waterQualityManagementPlanVerifyTreatmentBMPs.Select(x => new WaterQualityManagementPlanVerifyTreatmentBMPSimple(x)).OrderBy(x => x.TreatmentBMPName).ToList();

            
        }

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, bool deleteStructuralDocumentFile, List<WaterQualityManagementPlanVerifyQuickBMPSimple> waterQualityManagementPlanVerifyQuickBMPSimples, List<WaterQualityManagementPlanVerifyTreatmentBMPSimple> waterQualityManagementPlanVerifyTreatmentBMPSimples, Person currentPerson)
        {
            base.UpdateModels(waterQualityManagementPlan,
                 waterQualityManagementPlanVerify, waterQualityManagementPlanVerifyQuickBMPSimples, waterQualityManagementPlanVerifyTreatmentBMPSimples, currentPerson);
        }


     
    }
}
