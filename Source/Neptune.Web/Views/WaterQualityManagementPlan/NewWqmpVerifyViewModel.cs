using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class NewWqmpVerifyViewModel : FormViewModel, IValidatableObject
    {

        public int WaterQualityManagementPlanID { get; set; }
        public int? WaterQualityManagementPlanVerifyID { get; set;  }

        [Required]
        public int WaterQualityManagementPlanVerifyTypeID { get; set; }

        [Required]
        public int WaterQualityManagementPlanVisitStatusID { get; set; }

        [DisplayName("Structural BMP O&M Document")]
        [SitkaFileExtensions("pdf|zip|doc|docx|xls|xlsx|jpg|png")]
        public HttpPostedFileBase StructuralDocumentFile { get; set; }

        [Required]
        public int WaterQualityManagementPlanVerifyStatusID { get; set; }
        public string EnforcementOrFollowupActions { get; set; }
        public string SourceControlCondition { get; set; }

        public Models.WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
        public List<WaterQualityManagementPlanVerifyQuickBMPSimple> WaterQualityManagementPlanVerifyQuickBMPSimples { get; set; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMPSimple> WaterQualityManagementPlanVerifyTreatmentBMPSimples { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public NewWqmpVerifyViewModel()
        {
        }

        public NewWqmpVerifyViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan, List<QuickBMP> quickBMPs, List<Models.TreatmentBMP> treatmentBMPs, Person currentPerson)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            WaterQualityManagementPlanVerifyID = ModelObjectHelpers.NotYetAssignedID;
            var isDraft = true;
            WaterQualityManagementPlanVerify = new WaterQualityManagementPlanVerify(
                waterQualityManagementPlan.WaterQualityManagementPlanID,
                WaterQualityManagementPlanVerifyTypeID,
                WaterQualityManagementPlanVisitStatusID,
                currentPerson.PersonID,
                DateTime.Now,
                isDraft);


            WaterQualityManagementPlanVerifyQuickBMPSimples = quickBMPs?.Select(x => new WaterQualityManagementPlanVerifyQuickBMPSimple(x)).ToList();
            WaterQualityManagementPlanVerifyTreatmentBMPSimples = treatmentBMPs?.Select(x => new WaterQualityManagementPlanVerifyTreatmentBMPSimple(x)).ToList();
        }

        public virtual void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan,
            WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<WaterQualityManagementPlanVerifyQuickBMPSimple> waterQualityManagementPlanVerifyQuickBMPSimples, List<WaterQualityManagementPlanVerifyTreatmentBMPSimple> waterQualityManagementPlanVerifyTreatmentBMPSimples, Person currentPerson)
        {
            if (StructuralDocumentFile != null) { 
                var fileResource = FileResource.CreateNewFromHttpPostedFile(StructuralDocumentFile, currentPerson);
                waterQualityManagementPlanVerify.FileResource = fileResource;
                HttpRequestStorage.DatabaseEntities.AllFileResources.Add(fileResource);
            }


            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID = WaterQualityManagementPlanVerifyID ?? ModelObjectHelpers.NotYetAssignedID;
            waterQualityManagementPlanVerify.EnforcementOrFollowupActions = EnforcementOrFollowupActions;
            waterQualityManagementPlanVerify.SourceControlCondition = SourceControlCondition;


            var waterQualityManagementPlanVerifyQuickBMPsInDatabase = HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyQuickBMPs.Local;
            var waterQualityManagementPlanVerifyQuickBMPsToUpdate = waterQualityManagementPlanVerifyQuickBMPSimples?.Select(x => new WaterQualityManagementPlanVerifyQuickBMP(x,
                waterQualityManagementPlan.TenantID, waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID)).ToList();

            waterQualityManagementPlanVerifyQuickBMPsInDatabase.Merge(waterQualityManagementPlanVerifyQuickBMPsToUpdate, waterQualityManagementPlanVerifyQuickBMPsInDatabase, (x, y) => x.WaterQualityManagementPlanVerifyQuickBMPID == y.WaterQualityManagementPlanVerifyQuickBMPID,
                (x, y) =>
                {
                    x.IsAdequate = y.IsAdequate;
                    x.WaterQualityManagementPlanVerifyQuickBMPNote = y.WaterQualityManagementPlanVerifyQuickBMPNote;
                });


            var waterQualityManagementPlanVerifyTreatmentBMPsInDatabase = HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyTreatmentBMPs.Local;
            var waterQualityManagementPlanVerifyTreatmentBMPsToUpdate = waterQualityManagementPlanVerifyTreatmentBMPSimples?.Select(x => new WaterQualityManagementPlanVerifyTreatmentBMP(x,
                waterQualityManagementPlan.TenantID, waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID)).ToList();


            waterQualityManagementPlanVerifyTreatmentBMPsInDatabase.Merge(waterQualityManagementPlanVerifyTreatmentBMPsToUpdate, waterQualityManagementPlanVerifyTreatmentBMPsInDatabase, (x, y) => x.WaterQualityManagementPlanVerifyTreatmentBMPID == y.WaterQualityManagementPlanVerifyTreatmentBMPID,
                (x, y) =>
                {
                    x.IsAdequate = y.IsAdequate;
                    x.WaterQualityManagementPlanVerifyTreatmentBMPNote = y.WaterQualityManagementPlanVerifyTreatmentBMPNote;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            return validationResults;
        }
    }
}
